using MarketLink.Admin.Data;
using MarketLink.Admin.Models.Entities;
using MarketLink.Admin.Models.Enums;
using MarketLinkedWebsite.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketLink.Admin.Controllers;

/// <summary>System configuration: product categories and announcements.</summary>
[Authorize]
public class SettingsController(ApplicationDbContext db) : Controller
{
    public async Task<IActionResult> Index()
    {
        ViewBag.Categories = await db.Categories.AsNoTracking().OrderBy(c => c.Name).ToListAsync();
        ViewBag.Announcements = await db.Announcements.AsNoTracking().OrderByDescending(a => a.PublishedAt).ToListAsync();
        return View();
    }

    // ---------- Categories ----------

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> AddCategory(string name)
    {
        name = (name ?? string.Empty).Trim();
        if (name.Length == 0 || name.Length > 60)
            return Fail("Enter a category name (up to 60 characters).");

        if (await db.Categories.AnyAsync(c => c.Name == name))
            return Fail("That category already exists.");

        db.Categories.Add(new Category { Name = name });
        await db.SaveChangesAsync();

        TempData["Success"] = "Category added";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> RenameCategory(int id, string name)
    {
        name = (name ?? string.Empty).Trim();
        if (name.Length == 0 || name.Length > 60)
            return Fail("Enter a category name (up to 60 characters).");

        var category = await db.Categories.FindAsync(id);
        if (category is null) return NotFound();

        if (await db.Categories.AnyAsync(c => c.Name == name && c.Id != id))
            return Fail("That category already exists.");

        category.Name = name;
        await db.SaveChangesAsync();

        TempData["Success"] = "Category saved";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await db.Categories.FindAsync(id);
        if (category is null) return NotFound();

        if (await db.Products.AnyAsync(p => p.CategoryId == id))
            return Fail("This category still has products. Move or remove them first.");

        db.Categories.Remove(category);
        await db.SaveChangesAsync();

        TempData["Success"] = "Category deleted";
        return RedirectToAction(nameof(Index));
    }

    // ---------- Announcements ----------

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> PublishAnnouncement(string title, string body, AnnouncementAudience audience)
    {
        title = (title ?? string.Empty).Trim();
        body = (body ?? string.Empty).Trim();

        if (title.Length == 0 || body.Length == 0)
            return Fail("Enter a title and the announcement text.");
        if (title.Length > 120 || body.Length > 2000)
            return Fail("The title or text is too long.");

        db.Announcements.Add(new Announcement
        {
            Title = title,
            Body = body,
            Audience = audience,
            PublishedAt = DateTime.UtcNow
        });
        await db.SaveChangesAsync();

        TempData["Success"] = "Announcement published";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteAnnouncement(int id)
    {
        var announcement = await db.Announcements.FindAsync(id);
        if (announcement is null) return NotFound();

        db.Announcements.Remove(announcement);
        await db.SaveChangesAsync();

        TempData["Success"] = "Announcement deleted";
        return RedirectToAction(nameof(Index));
    }

    private IActionResult Fail(string message)
    {
        TempData["Error"] = message;
        return RedirectToAction(nameof(Index));
    }
}
