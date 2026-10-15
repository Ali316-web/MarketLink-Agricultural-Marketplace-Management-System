using MarketLink.Admin.Data;
using MarketLink.Admin.Models.Entities;
using MarketLink.Admin.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketLink.Admin.Controllers;

[Authorize]
public class MarketsController(ApplicationDbContext db) : Controller
{
    private const string BindFields =
        "Id,Name,Address,OpenTime,CloseTime,Latitude,Longitude,Provider,MapEmbedUrl";

    public async Task<IActionResult> Index() =>
        View(await db.Markets.AsNoTracking().Include(m => m.Farmers).OrderBy(m => m.Name).ToListAsync());

    [HttpGet]
    public IActionResult Create() =>
        View("Form", new Market { OpenTime = new TimeOnly(8, 0), CloseTime = new TimeOnly(14, 0) });

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind(BindFields)] Market market, MarketDays[] days)
    {
        market.OpenDays = Combine(days);
        Validate(market);
        if (!ModelState.IsValid) return View("Form", market);

        market.Id = 0;
        db.Markets.Add(market);
        await db.SaveChangesAsync();

        TempData["Success"] = "Market added";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var market = await db.Markets.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
        return market is null ? NotFound() : View("Form", market);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind(BindFields)] Market input, MarketDays[] days)
    {
        if (id != input.Id) return NotFound();

        input.OpenDays = Combine(days);
        Validate(input);
        if (!ModelState.IsValid) return View("Form", input);

        var market = await db.Markets.FindAsync(id);
        if (market is null) return NotFound();

        market.Name = input.Name;
        market.Address = input.Address;
        market.OpenDays = input.OpenDays;
        market.OpenTime = input.OpenTime;
        market.CloseTime = input.CloseTime;
        market.Latitude = input.Latitude;
        market.Longitude = input.Longitude;
        market.Provider = input.Provider;
        market.MapEmbedUrl = input.MapEmbedUrl;
        await db.SaveChangesAsync();

        TempData["Success"] = "Market saved";
        return RedirectToAction(nameof(Index));
    }

    /// <summary>Removes the market; its farmers stay on the platform as "Unassigned".</summary>
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var market = await db.Markets.FindAsync(id);
        if (market is null) return NotFound();

        db.Markets.Remove(market);
        await db.SaveChangesAsync();

        TempData["Success"] = "Market removed";
        return RedirectToAction(nameof(Index));
    }

    private static MarketDays Combine(MarketDays[]? days) =>
        (days ?? []).Aggregate(MarketDays.None, (all, d) => all | d);

    private void Validate(Market m)
    {
        if (m.OpenDays == MarketDays.None)
            ModelState.AddModelError(nameof(Market.OpenDays), "Choose at least one open day.");

        if (m.CloseTime <= m.OpenTime)
            ModelState.AddModelError(nameof(Market.CloseTime), "Closing time must be later than opening time.");
    }
}
