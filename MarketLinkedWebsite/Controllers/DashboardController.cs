using MarketLink.Admin.Data;
using MarketLink.Admin.Models.Enums;
using MarketLinkedWebsite.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketLink.Admin.Controllers;

[Authorize]
public class DashboardController(ApplicationDbContext db) : Controller
{
    public async Task<IActionResult> Index()
    {
        ViewBag.TotalFarmers = await db.Farmers.CountAsync();
        ViewBag.TotalCustomers = await db.Customers.CountAsync();
        ViewBag.TotalMarkets = await db.Markets.CountAsync();
        ViewBag.TotalOrders = await db.Orders.CountAsync();

        // Orders per week for the last 8 weeks, oldest first.
        var today = DateTime.UtcNow.Date;
        var since = today.AddDays(-7 * 8);
        var placed = await db.Orders.AsNoTracking()
            .Where(o => o.PlacedAt >= since)
            .Select(o => o.PlacedAt)
            .ToListAsync();

        ViewBag.OrdersPerWeek = Enumerable.Range(0, 8)
            .Select(i =>
            {
                var from = since.AddDays(7 * i);
                var to = from.AddDays(7);
                return placed.Count(d => d >= from && d < to);
            })
            .ToList();

        ViewBag.FarmersAwaitingApproval = await db.Farmers.AsNoTracking()
            .Include(f => f.Market)
            .Where(f => f.Status == FarmerStatus.Pending)
            .OrderBy(f => f.JoinedOn)
            .ToListAsync();

        ViewBag.RecentOrders = await db.Orders.AsNoTracking()
            .Include(o => o.Customer).Include(o => o.Farmer)
            .OrderByDescending(o => o.PlacedAt)
            .Take(6)
            .ToListAsync();

        ViewBag.LatestAnnouncements = await db.Announcements.AsNoTracking()
            .OrderByDescending(a => a.PublishedAt)
            .Take(3)
            .ToListAsync();

        return View();
    }
}
