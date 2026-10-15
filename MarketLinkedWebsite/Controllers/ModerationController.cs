using MarketLink.Admin.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketLink.Admin.Controllers;

[Authorize]
public class ModerationController(ApplicationDbContext db) : Controller
{
    /// <param name="tab">"products" or "reviews".</param>
    public async Task<IActionResult> Index(string tab = "products")
    {
        ViewBag.Tab = tab == "reviews" ? "reviews" : "products";

        ViewBag.Products = await db.Products.AsNoTracking()
            .Include(p => p.Farmer).Include(p => p.Category)
            .OrderBy(p => p.Name)
            .ToListAsync();

        ViewBag.Reviews = await db.Reviews.AsNoTracking()
            .Include(r => r.Customer).Include(r => r.Farmer)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();

        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await db.Products.FindAsync(id);
        if (product is null) return NotFound();

        db.Products.Remove(product);
        await db.SaveChangesAsync();

        TempData["Success"] = "Listing removed";
        return RedirectToAction(nameof(Index), new { tab = "products" });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteReview(int id)
    {
        var review = await db.Reviews.FindAsync(id);
        if (review is null) return NotFound();

        db.Reviews.Remove(review);
        await db.SaveChangesAsync();

        TempData["Success"] = "Review removed";
        return RedirectToAction(nameof(Index), new { tab = "reviews" });
    }
}
