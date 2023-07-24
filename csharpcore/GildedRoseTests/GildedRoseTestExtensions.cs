namespace GildedRoseTests;

internal static class GildedRoseTestExtensions
{
    public static GildedRose RunNDays(this GildedRose app, int days)
    {
        int counter = 0;
        while (counter < days)
        {
            counter++;
            app.UpdateQuality();
        }        
        return app;
    }

    public static void VerifyFirstItem(this GildedRose app, int expectedQuality, int expectedSellIn)
    {
        Item item = app.Items.First();
        item.Quality.Should().Be(expectedQuality);
        item.SellIn.Should().Be(expectedSellIn);
    }
}