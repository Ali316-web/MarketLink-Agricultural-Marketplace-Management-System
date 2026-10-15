using MarketLink.Admin.Data;
using MarketLink.Admin.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketLink.Admin.Controllers;

[Authorize]
public class CustomersController(ApplicationDbContext db) : Controller
{
    public async Task<IActionResult> Index(string? search, CustomerStatus? status)
    {
        var query = db.Customers.AsNoTracking().Include(c => c.Orders).AsQueryable();

        if (status.HasValue)
            query = query.Where(c => c.Status == status.Value);

        if (!string.IsNullOrWhiteSpace(search))
        {
            var q = search.Trim();
            query = query.Where(c => c.Name.Contains(q) || c.Email.Contains(q));
        }

        ViewBag.Search = search;
        ViewBag.Status = status;

        return View(await query.OrderBy(c => c.Name).ToListAsync());
    }

    public async Task<IActionResult> Details(int id)
    {
        var customer = await db.Customers.AsNoTracking()
            .Include(c => c.Orders).ThenInclude(o => o.Farmer)
            .FirstOrDefaultAsync(c => c.Id == id);

        return customer is null ? NotFound() : View(customer);
    }

    /// <summary>Activates an inactive account or deactivates an active one.</summary>
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleStatus(int id)
    {
        var customer = await db.Customers.FindAsync(id);
        if (customer is null) return NotFound();

        var activate = customer.Status == CustomerStatus.Inactive;
        customer.Status = activate ? CustomerStatus.Active : CustomerStatus.Inactive;
        await db.SaveChangesAsync();

        TempData["Success"] = activate ? "Account activated" : "Account deactivated";
        return RedirectToAction(nameof(Index));
    }
}
