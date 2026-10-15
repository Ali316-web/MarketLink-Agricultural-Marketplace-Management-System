namespace MarketLink.Admin.Models.Enums;

/// <summary>Days a market is open. Stored as a single flags value (e.g. Wednesday | Saturday).</summary>
[Flags]
public enum MarketDays : byte
{
    None = 0,
    Monday = 1,
    Tuesday = 2,
    Wednesday = 4,
    Thursday = 8,
    Friday = 16,
    Saturday = 32,
    Sunday = 64
}
