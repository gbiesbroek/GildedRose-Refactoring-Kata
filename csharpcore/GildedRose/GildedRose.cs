namespace GildedRoseKata
{
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
                bool isAfterSellByDate = item.SellIn <= 0;
                int qualityChange = item.Name switch
                {
                    ItemNames.Sulfuras      => 0,
                    ItemNames.ConjuredMana  => isAfterSellByDate ? -4 : -2,
                    ItemNames.AgedBrie      => isAfterSellByDate ? 2 : 1,
                    ItemNames.BackstagePass => BackStageQualityChange(item),
                    _                       => isAfterSellByDate ? -2 : -1            
                };
                if (item.Name != ItemNames.Sulfuras)
                {
                    item.SellIn--;
                    item.Quality = Math.Clamp(item.Quality + qualityChange, _minQuality, _maxQuality);
                }                
            }
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
}