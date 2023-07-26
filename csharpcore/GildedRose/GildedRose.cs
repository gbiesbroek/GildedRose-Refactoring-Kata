namespace GildedRoseKata;

public class GildedRose
{
    public List<Item> Items { get; private init; }

    public GildedRose(List<Item> Items) => 
        this.Items = Items;

    public void UpdateQuality()
    {
        foreach (Item item in Items)
        {
            var adjustment = item.FindItemAdjustment();
            adjustment.Apply(item);
        }            
    }
}