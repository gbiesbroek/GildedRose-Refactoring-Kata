namespace GildedRoseKata;

internal static class ItemExtensions
{
    private static readonly ItemAdjustment _defaultAdjustment = new NormalItemAdjustment();
    private static readonly Dictionary<string, ItemAdjustment> _rules = new()
    {
        { ItemNames.AgedBrie, new BrieAdjustment() },
        { ItemNames.BackstagePass, new BackstagePassAdjustment() },
        { ItemNames.Sulfuras, new LegendaryItemAdjustment() },
        { ItemNames.ConjuredMana, new ConjuredItemAdjustment() }        
    };

    public static ItemAdjustment FindItemAdjustment(this Item item) =>    
        _rules.ContainsKey(item.Name) 
            ? _rules[item.Name]
            : _defaultAdjustment;
}