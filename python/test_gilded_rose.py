# -*- coding: utf-8 -*-
from unittest import TestCase, main as unittests_main
from dataclasses import dataclass
from gilded_rose import Item, GildedRose, ItemNames

@dataclass
class TestData:
    run_n_days: int
    item: Item
    expected_quality: int
    expected_sell_in: int

class normal_items(TestCase):
    def test_quality_decreases_by_1_before_sell_by_date(self):
        test_data = [
            TestData(1, Item(ItemNames.DEXTERITY_VEST, 10, 50), 49, 9),
            TestData(5, Item(ItemNames.MONGOOSE_ELIXER, 10, 50), 45, 5),
            TestData(10, Item(ItemNames.MONGOOSE_ELIXER, 10, 50), 40, 0)
        ]
        _run_tests(self, test_data)

    def test_quality_decreases_by_2_after_sell_by_date(self):
        test_data = [
            TestData(11, Item(ItemNames.MONGOOSE_ELIXER, 10, 50), 38, -1),
            TestData(25, Item(ItemNames.DEXTERITY_VEST, 10, 50), 10, -15),
            TestData(30, Item(ItemNames.DEXTERITY_VEST, 10, 50), 0, -20)
        ]
        _run_tests(self, test_data)

    def test_quality_does_not_go_below_0(self):
        test_data = [
            TestData(31, Item(ItemNames.MONGOOSE_ELIXER, 10, 50), 0, -21),
            TestData(50, Item(ItemNames.DEXTERITY_VEST, 10, 50), 0, -40)
        ]
        _run_tests(self, test_data)

class aged_brie(TestCase):  
    def _item(self) -> Item:
        return Item(ItemNames.AGED_BRIE, 10, 5) 

    def test_quality_increases_by_1_before_sell_by_date(self):
        test_data = [
             TestData(1, self._item(), 6, 9),
             TestData(2, self._item(), 7, 8),
             TestData(10, self._item(), 15, 0)
        ]
        _run_tests(self, test_data)

    def test_quality_increases_by_2_after_sell_by_date(self):
        test_data = [
             TestData(11, self._item(), 17, -1),
             TestData(20, self._item(), 35, -10),
             TestData(25, self._item(), 45, -15),
             TestData(27, self._item(), 49, -17)
        ]
        _run_tests(self, test_data)    
    
    def test_quality_does_not_go_above_50(self):
        test_data = [
             TestData(28, self._item(), 50, -18),
             TestData(50, self._item(), 50, -40)
        ]
        _run_tests(self, test_data)

    '''
    [InlineData(28, 50, -18)]
    [InlineData(50, 50, -40)]
    public void Quality_does_not_increase_above_50(int days, int expectedQuality, int expectedSellIn)
    {
        _sut.RunNDays(days);
        _sut.VerifyFirstItem(expectedQuality, expectedSellIn);
    }
    '''
           
class sulfuras(TestCase):
     def test_quality_should_remain_80_and_sell_by_date_stays_at_starting_value(self):
        test_data = [
            TestData(10, Item(ItemNames.SULFURAS, 10, 80), 80, 10),
            TestData(20, Item(ItemNames.SULFURAS, 10, 80), 80, 10)
        ]
        _run_tests(self, test_data)    

class backstage_passes(TestCase):
    def _item(self) -> Item:
        return Item(ItemNames.BACKSTAGE_PASS, 15, 25)

    def test_quality_increases_by_1_until_10_days_before_sell_by_date(self):
        test_data = [
            TestData(1, self._item(), 26, 14),
            TestData(2, self._item(), 27, 13),
            TestData(5, self._item(), 30, 10)
        ]
        _run_tests(self, test_data)

    def test_quality_increases_by_2_between_10_and_5_days_before_sell_by_date(self):
        test_data = [
            TestData(6, self._item(), 32, 9),
            TestData(7, self._item(), 34, 8),
            TestData(10, self._item(), 40, 5)
        ]
        _run_tests(self, test_data)

    def test_quality_increases_by_3_between_5_and_0_days_before_sell_by_date(self):
        test_data = [
            TestData(11, self._item(), 43, 4),
            TestData(12, self._item(), 46, 3),
            TestData(13, self._item(), 49, 2)
        ]
        _run_tests(self, test_data)

    def test_quality_drops_to_0_after_sell_by_date(self):
        test_data = [
            TestData(16, self._item(), 0, -1),
            TestData(30, self._item(), 0, -15)
        ]
        _run_tests(self, test_data)

    def test_quality_does_not_go_above_50(self):
        test_data = [
            TestData(15, self._item(), 50, 0)
        ]
        _run_tests(self, test_data)

class conjured_item(TestCase):
    def _item(self) -> Item:
        return Item(ItemNames.CONJURED_MANA, 5, 25)
    
    def test_quality_decreases_by_2_before_sell_by_date(self):
        test_data = [
            TestData(1, self._item(), 23, 4),
            TestData(2, self._item(), 21, 3),
            TestData(5, self._item(), 15, 0)
        ]
        _run_tests(self, test_data)

    def test_quality_decreases_by_4_after_sell_by_date(self):
        test_data = [
            TestData(6, self._item(), 11, -1),
            TestData(7, self._item(), 7, -2),
            TestData(8, self._item(), 3, -3)
        ]
        _run_tests(self, test_data)

    def test_quality_does_not_go_below_0(self):
        test_data = [
            TestData(9, self._item(), 0, -4)
        ]
        _run_tests(self, test_data)

def _run_tests(test: TestCase, test_data: list[TestData]):
        for testcase in test_data:            
            _run_n_times_with_item(test, testcase)

def _run_n_times_with_item(test: TestCase, test_data: TestData):
        '''
        Creates an instance of GildedRose as system under test(sut), with the item, updating it for n_times days. 
        At the end of the update run, verify the end state
        '''
        sut = GildedRose([test_data.item])
        for _ in range(test_data.run_n_days):
            sut.update_quality()
        
        _verify_item(test, test_data, sut.items[0])        

def _verify_item(test: TestCase, test_data: TestData, to_check: Item):
    test.assertEqual(to_check.quality, test_data.expected_quality)
    test.assertEqual(to_check.sell_in, test_data.expected_sell_in)

if __name__ == '__main__':
    unittests_main()