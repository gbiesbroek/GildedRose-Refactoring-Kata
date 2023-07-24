namespace GildedRoseTests;

public class GildedRoseTests
{
    public class DexterityVest
    {
        private readonly GildedRose _sut;

        public DexterityVest()
        {
            List<Item> items = new() { new() { Name = ItemNames.DexteryVest, SellIn = 10, Quality = 50 } };
            _sut = new(items);
        }

        [Theory]
        [InlineData(1, 49, 9)]
        [InlineData(5, 45, 5)]
        [InlineData(10, 40, 0)]
        public void Decreases_quality_by_1_before_sell_by_date(int days, int expectedQuality, int expectedSellIn)
        {
            _sut.RunNDays(days);
            _sut.VerifyFirstItem(expectedQuality, expectedSellIn);
        }

        [Theory]
        [InlineData(11, 38, -1)]
        [InlineData(25, 10, -15)]
        [InlineData(30, 0, -20)]
        public void Decreases_quality_by_2_after_sell_by_date(int days, int expectedQuality, int expectedSellIn)
        {
            _sut.RunNDays(days);
            _sut.VerifyFirstItem(expectedQuality, expectedSellIn);
        }

        [Theory]
        [InlineData(31, 0, -21)]
        [InlineData(50, 0, -40)]
        public void Quality_does_not_decrease_below_0(int days, int expectedQuality, int expectedSellIn)
        {
            _sut.RunNDays(days);
            _sut.VerifyFirstItem(expectedQuality, expectedSellIn);
        }
    }

    public class AgedBrie
    {
        private readonly GildedRose _sut;

        public AgedBrie()
        {
            List<Item> items = new() { new() { Name = ItemNames.AgedBrie, SellIn = 10, Quality = 5 } };
            _sut = new(items);
        }

        [Theory]
        [InlineData(1, 6, 9)]
        [InlineData(2, 7, 8)]
        [InlineData(10, 15, 0)]
        public void Increases_quality_by_1_before_sell_by_date(int days, int expectedQuality, int expectedSellIn)
        {
            _sut.RunNDays(days);
            _sut.VerifyFirstItem(expectedQuality, expectedSellIn);
        }

        [Theory]
        [InlineData(11, 17, -1)]
        [InlineData(20, 35, -10)]
        [InlineData(25, 45, -15)]
        [InlineData(27, 49, -17)]
        public void Increases_quality_by_2_after_sell_by_date(int days, int expectedQuality, int expectedSellIn)
        {
            _sut.RunNDays(days);
            _sut.VerifyFirstItem(expectedQuality, expectedSellIn);
        }

        [Theory]
        [InlineData(28, 50, -18)]
        [InlineData(50, 50, -40)]
        public void Quality_does_not_increase_above_50(int days, int expectedQuality, int expectedSellIn)
        {
            _sut.RunNDays(days);
            _sut.VerifyFirstItem(expectedQuality, expectedSellIn);
        }
    }

    public class Sulfuras
    {
        private readonly GildedRose _sut;

        public Sulfuras()
        {
            List<Item> items = new() { new() { Name = ItemNames.Sulfuras, SellIn = 10, Quality = 80 } };
            _sut = new(items);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(50)]
        public void Quality_should_remain_80_and_SellIn_stays_at_starting_value(int days)
        {
            _sut.RunNDays(days);
            _sut.VerifyFirstItem(80, 10);
        }
    }

    public class BackstagePasses
    {
        private readonly GildedRose _sut;

        public BackstagePasses()
        {
            List<Item> items = new() { new() { Name = ItemNames.BackstagePass, SellIn = 15, Quality = 25 } };
            _sut = new(items);
        }

        [Theory]
        [InlineData(1, 26, 14)]
        [InlineData(2, 27, 13)]
        [InlineData(5, 30, 10)]
        public void Quality_increase_by_1_until_10_days_before_sell_by_date(int days, int expectedQuality, int expectedSellIn)
        {
            _sut.RunNDays(days);
            _sut.VerifyFirstItem(expectedQuality, expectedSellIn);
        }

        [Theory]
        [InlineData(6, 32, 9)]
        [InlineData(7, 34, 8)]
        [InlineData(10, 40, 5)]
        public void Quality_increase_by_2_between_10_and_5_days_before_sell_by_date(int days, int expectedQuality, int expectedSellIn)
        {
            _sut.RunNDays(days);
            _sut.VerifyFirstItem(expectedQuality, expectedSellIn);
        }

        [Theory]
        [InlineData(11, 43, 4)]
        [InlineData(12, 46, 3)]
        [InlineData(13, 49, 2)]
        public void Quality_increase_by_3_between_5_and_0_days_before_sell_by_date(int days, int expectedQuality, int expectedSellIn)
        {
            _sut.RunNDays(days);
            _sut.VerifyFirstItem(expectedQuality, expectedSellIn);
        }

        [Theory]
        [InlineData(16, 0, -1)]
        [InlineData(30, 0, -15)]
        public void Quality_drops_to_0_after_sell_by_date(int days, int expectedQuality, int expectedSellIn)
        {
            _sut.RunNDays(days);
            _sut.VerifyFirstItem(expectedQuality, expectedSellIn);
        }

        [Fact]
        public void Quality_does_not_go_above_50()
        {
            _sut.RunNDays(15);
            _sut.VerifyFirstItem(50, 0);
        }
    }

    public class ConjuredItem
    {
        private readonly GildedRose _sut;

        public ConjuredItem()
        {
            List<Item> items = new() { new() { Name = ItemNames.ConjuredMana, SellIn = 5, Quality = 25 } };
            _sut = new(items);
        }

        [Theory]
        [InlineData(1, 23, 4)]
        [InlineData(2, 21, 3)]
        [InlineData(5, 15, 0)]
        public void Quality_decreased_by_2_before_sell_by_date(int days, int expectedQuality, int expectedSellIn)
        {
            _sut.RunNDays(days);
            _sut.VerifyFirstItem(expectedQuality, expectedSellIn);
        }

        [Theory]
        [InlineData(6, 11, -1)]
        [InlineData(7, 7, -2)]
        [InlineData(8, 3, -3)]
        public void Quality_decreased_by_4_after_sell_by_date(int days, int expectedQuality, int expectedSellIn)
        {
            _sut.RunNDays(days);
            _sut.VerifyFirstItem(expectedQuality, expectedSellIn);
        }

        [Fact]
        public void Quality_does_not_go_below_0()
        {
            _sut.RunNDays(9);
            _sut.VerifyFirstItem(0, -4);
        }
    }
}