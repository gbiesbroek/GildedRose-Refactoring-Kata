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
        [InlineData(1, 49)]
        [InlineData(5, 45)]
        [InlineData(10, 40)]
        public void Decreases_quality_by_1_before_sell_by_date(int days, int expectedQuality)
        {
            _sut.RunNDays(days);
            _sut.Items.First().Quality.Should().Be(expectedQuality);
        }

        [Theory]
        [InlineData(11, 38)]
        [InlineData(25, 10)]
        [InlineData(30, 0)]
        public void Decreases_quality_by_2_after_sell_by_date(int days, int expectedQuality)
        {
            _sut.RunNDays(days);
            _sut.Items.First().Quality.Should().Be(expectedQuality);
        }

        [Theory]
        [InlineData(31, 0)]
        [InlineData(50, 0)]
        public void Quality_does_not_decrease_below_0(int days, int expectedQuality)
        {
            _sut.RunNDays(days);
            _sut.Items.First().Quality.Should().Be(expectedQuality);
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
        [InlineData(1, 6)]
        [InlineData(2, 7)]
        [InlineData(10, 15)]
        public void Increases_quality_by_1_before_sell_by_date(int days, int expectedQuality)
        {            
            _sut.RunNDays(days);
            _sut.Items.First().Quality.Should().Be(expectedQuality);
        }

        [Theory]
        [InlineData(11, 17)]
        [InlineData(20, 35)]
        [InlineData(25, 45)]
        [InlineData(27, 49)]
        public void Increases_quality_by_2_after_sell_by_date(int days, int expectedQuality)
        {
            _sut.RunNDays(days);
            _sut.Items.First().Quality.Should().Be(expectedQuality);
        }

        [Theory]
        [InlineData(28, 50)]
        [InlineData(50, 50)]
        [InlineData(60, 50)]
        public void Quality_does_not_increase_above_50(int days, int expectedQuality)
        {
            _sut.RunNDays(days);
            _sut.Items.First().Quality.Should().Be(expectedQuality);
        }
    }

    public class Sulfuras
    {
        private readonly GildedRose _sut;

        public Sulfuras()
        {
            List<Item> items = new() { new() { Name = ItemNames.Sulfuras, SellIn = 10, Quality = 30 } };
            _sut = new(items);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(50)]
        public void Quality_and_SellIn_stay_at_starting_value(int days)
        {
            _sut.RunNDays(days);
            Item result = _sut.Items.First();
            result.Quality.Should().Be(30);
            result.SellIn.Should().Be(10);
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
        [InlineData(1, 26)]
        [InlineData(2, 27)]
        [InlineData(5, 30)]
        public void Quality_increase_by_1_until_10_days_before_sell_by_date(int days, int expectedQuality)
        {
            _sut.RunNDays(days);
            _sut.Items.First().Quality.Should().Be(expectedQuality);
        }

        [Theory]
        [InlineData(6, 32)]
        [InlineData(7, 34)]
        [InlineData(10, 40)]
        public void Quality_increase_by_2_between_10_and_5_days_before_sell_by_date(int days, int expectedQuality)
        {
            _sut.RunNDays(days);
            _sut.Items.First().Quality.Should().Be(expectedQuality);
        }

        [Theory]
        [InlineData(11, 43)]
        [InlineData(12, 46)]
        [InlineData(13, 49)]
        public void Quality_increase_by_3_between_5_and_0_days_before_sell_by_date(int days, int expectedQuality)
        {
            _sut.RunNDays(days);
            _sut.Items.First().Quality.Should().Be(expectedQuality);
        }

        [Theory]
        [InlineData(16, 0)]
        [InlineData(30, 0)]
        public void Quality_drops_to_0_after_sell_by_date(int days, int expectedQuality)
        {
            _sut.RunNDays(days);
            _sut.Items.First().Quality.Should().Be(expectedQuality);
        }

        [Fact]
        public void Quality_does_not_go_above_50()
        {
            _sut.RunNDays(15);
            _sut.Items.First().Quality.Should().Be(50);
        }
    }

    public class Conjured
    {

    }
}