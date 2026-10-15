
using MarketLink.Admin.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketLink.Admin.Models.Entities;

public class Market
{
    public int Id { get; set; }

    [Required, StringLength(120)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(250)]
    public string Address { get; set; } = string.Empty;

    [Display(Name = "Open days")]
    public MarketDays OpenDays { get; set; }

    [DataType(DataType.Time)]
    [Display(Name = "Opens")]
    public TimeOnly OpenTime { get; set; }

    [DataType(DataType.Time)]
    [Display(Name = "Closes")]
    public TimeOnly CloseTime { get; set; }

    [Range(-90, 90)]
    public double Latitude { get; set; }

    [Range(-180, 180)]
    public double Longitude { get; set; }

    [Display(Name = "Map provider")]
    public MapProvider Provider { get; set; } = MapProvider.OpenStreetMap;

    [Url, StringLength(1000)]
    [Display(Name = "Map embed URL")]
    public string? MapEmbedUrl { get; set; }

    public ICollection<Farmer> Farmers { get; set; } = new List<Farmer>();

    /// <summary>Link that opens this market's coordinates in the chosen map provider.</summary>
    [NotMapped]
    public string MapLink => Provider == MapProvider.GoogleMaps
        ? FormattableString.Invariant($"https://www.google.com/maps?q={Latitude},{Longitude}")
        : FormattableString.Invariant($"https://www.openstreetmap.org/?mlat={Latitude}&mlon={Longitude}#map=16/{Latitude}/{Longitude}");
}
