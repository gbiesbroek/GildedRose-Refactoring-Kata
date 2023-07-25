# -*- coding: utf-8 -*-
import unittest
from dataclasses import dataclass
from gilded_rose import Item, GildedRose, ItemNames

@dataclass
class TestData:
    run_n_days: int
    item: Item
    expected_quality: int
    expected_sell_in: int

class GildedRoseTest(unittest.TestCase):
    def test_normal_items_quality_decreases_by_1_before_sell_by_date(self):
        testData = [
             TestData(1, Item(ItemNames.DEXTERITY_VEST, 10, 50), 49, 9),
             TestData(5, Item(ItemNames.DEXTERITY_VEST, 10, 50), 45, 5),
             TestData(10, Item(ItemNames.DEXTERITY_VEST, 10, 50), 40, 0)
        ]
        for testcase in testData:
            self.run_n_times_with_item(testcase)

    def test_normal_items_quality_decreases_by_2_after_sell_by_date(self):
        testData = [
             TestData(11, Item(ItemNames.DEXTERITY_VEST, 10, 50), 38, -1),
             TestData(25, Item(ItemNames.DEXTERITY_VEST, 10, 50), 10, -15),
             TestData(30, Item(ItemNames.DEXTERITY_VEST, 10, 50), 0, -20)
        ]
        for testcase in testData:
            self.run_n_times_with_item(testcase)
    
    def run_n_times_with_item(self, testData: TestData) -> None:
        '''
        Creates an instance of GildedRose, with the item, updating it for n_times days. 
        At the end of the update run, verify the end state
        '''
        #print(f"\n-------start: { testData.item }--------\n")
        sut = GildedRose([testData.item])
        counter = 0
        while counter < testData.run_n_days:
            sut.update_quality()
            #print(sut.items[0])
            counter += 1
        
        result = sut.items[0]
        self.assertEqual(result.quality, testData.expected_quality)
        self.assertEqual(result.sell_in, testData.expected_sell_in)        
        
if __name__ == '__main__':
    unittest.main()