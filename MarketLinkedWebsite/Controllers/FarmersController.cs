using MarketLink.Admin.Data;
using MarketLink.Admin.Models.Enums;
using MarketLinkedWebsite.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketLink.Admin.Controllers;

[Authorize]
public class FarmersController(ApplicationDbContext db) : Controller
{
    public async Task<IActionResult> Index(string? search, FarmerStatus? status)
    {
        var query = db.Farmers.AsNoTracking()
            .Include(f => f.Market)
            .Include(f => f.Orders)
            .AsQueryable();

        if (status.HasValue)
            query = query.Where(f => f.Status == status.Value);

        if (!string.IsNullOrWhiteSpace(search))
        {
            var q = search.Trim();
            query = query.Where(f => f.StallName.Contains(q) || f.ContactName.Contains(q) || f.Email.Contains(q));
        }

        ViewBag.Search = search;
        ViewBag.Status = status;
        ViewBag.PendingCount = await db.Farmers.CountAsync(f => f.Status == FarmerStatus.Pending);

        return View(await query.OrderBy(f => f.StallName).ToListAsync());
    }

    public async Task<IActionResult> Details(int id)
    {
        var farmer = await db.Farmers.AsNoTracking()
            .Include(f => f.Market)
            .Include(f => f.Orders)
            .Include(f => f.Products).ThenInclude(p => p.Category)
            .FirstOrDefaultAsync(f => f.Id == id);

        return farmer is null ? NotFound() : View(farmer);
    }

    /// <summary>Approves a pending farmer or reinstates a suspended one.</summary>
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Approve(int id, string? returnUrl = null)
    {
        var farmer = await db.Farmers.FindAsync(id);
        if (farmer is null) return NotFound();

        var wasSuspended = farmer.Status == FarmerStatus.Suspended;
        farmer.Status = FarmerStatus.Approved;
        await db.SaveChangesAsync();

        TempData["Success"] = wasSuspended ? "Farmer reinstated" : "Farmer approved";
        return RedirectBack(returnUrl);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Suspend(int id, string? returnUrl = null)
    {
        var farmer = await db.Farmers.FindAsync(id);
        if (farmer is null) return NotFound();

        farmer.Status = FarmerStatus.Suspended;
        await db.SaveChangesAsync();

        TempData["Success"] = "Farmer suspended";
        return RedirectBack(returnUrl);
    }

    private IActionResult RedirectBack(string? returnUrl) =>
        !string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)
            ? LocalRedirect(returnUrl)
            : RedirectToAction(nameof(Index));
}
