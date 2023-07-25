namespace GildedRoseKata;

public class GildedRose
{
    private const int _minQuality = 0;
    private const int _maxQuality = 50;
    private const int _backstage_threshold_10 = 10;
    private const int _backstage_threshold_5 = 5;

    public List<Item> Items { get; private init; }

    public GildedRose(List<Item> Items)
    {
        this.Items = Items;
    }                   

    public void UpdateQuality()
    {
        foreach (Item item in Items)
        {
            if (item.Name == ItemNames.Sulfuras)
                continue;

            int qualityChange = DetermineQualityChange(item);
            --item.SellIn;
            item.Quality = Math.Clamp(item.Quality + qualityChange, _minQuality, _maxQuality);                            
        }
    }

    private static int DetermineQualityChange(Item item)
    {
        bool beforeSellDate = item.SellIn > 0;
        return item.Name switch
        {
            ItemNames.ConjuredMana  => beforeSellDate ? -2 : -4,
            ItemNames.AgedBrie      => beforeSellDate ? 1 : 2,
            ItemNames.BackstagePass => BackStageQualityChange(item),
            _                       => beforeSellDate ? -1 : -2
        };
    }

    private static int BackStageQualityChange(Item item) =>
        item.SellIn switch
        {
            <= 0                        => -item.Quality,
            <= _backstage_threshold_5   => 3,
            <= _backstage_threshold_10  => 2,
            _                           => 1
        };
}