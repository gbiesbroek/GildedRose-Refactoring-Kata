class Item:
    def __init__(self, name: str, sell_in: int, quality: int):
        self.name = name
        self.sell_in = sell_in
        self.quality = quality

    def __repr__(self):
        return "%s, %s, %s" % (self.name, self.sell_in, self.quality)

class ItemNames:
    DEXTERITY_VEST = "+5 Dexterity Vest"
    AGED_BRIE = "Aged Brie"
    MONGOOSE_ELIXER = "Elixir of the Mongoose"
    SULFURAS = "Sulfuras, Hand of Ragnaros"
    BACKSTAGE_PASS = "Backstage passes to a TAFKAL80ETC concert"
    CONJURED_MANA = "Conjured Mana Cake"
    DEFAULT = "Default"

class GildedRose(object):

    MAX_QUALITY = 50
    MIN_QUALITY = 0
    BACKSTAGE_PASS_THRESHOLD_10 = 10
    BACKSTAGE_PASS_THRESHOLD_5 = 5

    def __init__(self, items: list[Item]):
        self.items = items

    def _determine_quality_change(self, item: Item) -> int:
        before_sell_date = item.sell_in > 0
        change_values = {
            ItemNames.CONJURED_MANA : -2 if before_sell_date else -4,            
            ItemNames.BACKSTAGE_PASS: self._backstage_quality_change(item),
            ItemNames.AGED_BRIE     : 1 if before_sell_date else 2,
            ItemNames.DEFAULT       : -1 if before_sell_date else -2
        }
        search_key = item.name if item.name in change_values else ItemNames.DEFAULT        
        return change_values[search_key]

    def _backstage_quality_change(self, item: Item) -> int:
        if item.sell_in <= 0:
            return -item.quality
        elif item.sell_in <= self.BACKSTAGE_PASS_THRESHOLD_5:
            return 3
        elif item.sell_in <= self.BACKSTAGE_PASS_THRESHOLD_10:
            return 2
        else:
            return 1

    def update_quality(self):
        for item in self.items:
            if item.name == ItemNames.SULFURAS:
                continue
            
            quality_change = self._determine_quality_change(item)
            item.sell_in -= 1
            item.quality = max(self.MIN_QUALITY, min(item.quality + quality_change, self.MAX_QUALITY))   