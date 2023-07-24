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
}