namespace GildedRoseKata;

internal abstract class ItemAdjustment
{
    private const int _minQuality = 0;
    private const int _maxQuality = 50;

    internal abstract Func<Item, int> Rule { get; }
    internal virtual Item Apply(Item item)
    {
        int qualityAdjustment = Rule(item);
        item.Quality = Math.Clamp(item.Quality + qualityAdjustment, _minQuality, _maxQuality);
        --item.SellIn;
        return item;
    }
}

internal class NormalItemAdjustment : ItemAdjustment
{
    internal override Func<Item, int> Rule =>
        item => (item.SellIn > 0) ? -1 : -2;
}

internal class LegendaryItemAdjustment : ItemAdjustment
{
    internal override Func<Item, int> Rule => _ => 0;
    internal override Item Apply(Item item) => item;
}

internal class BrieAdjustment : ItemAdjustment
{
    internal override Func<Item, int> Rule => 
        item => (item.SellIn > 0) ? 1 : 2;
}

internal class ConjuredItemAdjustment : ItemAdjustment
{
    internal override Func<Item, int> Rule =>
        item => (item.SellIn > 0) ? -2 : -4;
}

internal class BackstagePassAdjustment : ItemAdjustment
{
    internal override Func<Item, int> Rule => 
        item => item.SellIn switch
        {
            <= 0    => -item.Quality,
            <= 5    => 3,
            <= 10   => 2,
            _       => 1
        };
}