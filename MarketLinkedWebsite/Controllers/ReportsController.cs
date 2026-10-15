using MarketLink.Admin.Data;
using MarketLink.Admin.Models.Entities;
using MarketLink.Admin.Models.Enums;
using MarketLinkedWebsite.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MarketLink.Admin.Controllers;

[Authorize]
public class ReportsController(ApplicationDbContext db) : Controller
{
    public async Task<IActionResult> Index()
    {
        var totalOrders = await db.Orders.CountAsync();
        var totalRevenue = await db.Orders
            .Where(o => o.Status == OrderStatus.Completed)
            .SumAsync(o => (decimal?)o.Total) ?? 0m;

        ViewBag.TotalOrders = totalOrders;
        ViewBag.TotalRevenue = totalRevenue;
        ViewBag.AverageOrderValue = totalOrders == 0 ? 0m : totalRevenue / totalOrders;

        var markets = await db.Markets.AsNoTracking()
            .Include(m => m.Farmers).ThenInclude(f => f.Orders)
            .ToListAsync();

        ViewBag.RevenueByMarket = markets
            .Select(m => (
                Market: m.Name,
                Revenue: m.Farmers.Sum(f => f.TotalRevenue),
                Orders: m.Farmers.Sum(f => f.TotalOrders)))
            .OrderByDescending(x => x.Revenue)
            .ToList();

        ViewBag.MostActiveFarmers = (await db.Farmers.AsNoTracking()
                .Include(f => f.Market).Include(f => f.Orders)
                .ToListAsync())
            .OrderByDescending(f => f.TotalOrders)
            .Take(5)
            .ToList();

        var reports = await db.GeneratedReports.AsNoTracking()
            .Include(r => r.GeneratedBy)
            .OrderByDescending(r => r.GeneratedAt)
            .ToListAsync();

        return View(reports);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Generate(ReportType type)
    {
        db.GeneratedReports.Add(new GeneratedReport
        {
            Type = type,
            GeneratedById = User.FindFirstValue(ClaimTypes.NameIdentifier)!,
            GeneratedAt = DateTime.UtcNow
        });
        await db.SaveChangesAsync();

        TempData["Success"] = "Report generated";
        return RedirectToAction(nameof(Index));
    }
}
