using System.Collections.Generic;

namespace GildedRoseKata
{
  public class GildedRose
  {
    static IList<Item> Items;

    public static void SetInventory(IList<Item> items)
		{
      GildedRose.Items = items;
		}

    // TODO: Deprecate this. keeping the constructor around for (imaginary) legacy code
    public GildedRose(IList<Item> items)
		{
      GildedRose.Items = items;
		}

    public static void UpdateQuality(IList<Item> items)
		{
      SetInventory(items);
      UpdateQuality();
		}

    public static void UpdateQuality()
    {
      for (var i = 0; i < Items.Count; i++)
      {
        if (Items[i].Name != "Aged Brie" && Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
        {
          if (Items[i].Quality > 0)
          {
            if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
            {
              Items[i].Quality = Items[i].Quality - 1;
            }
          }
        }
        else
        {
          if (Items[i].Quality < 50)
          {
            Items[i].Quality = Items[i].Quality + 1;

            if (Items[i].Name == "Backstage passes to a TAFKAL80ETC concert")
            {
              if (Items[i].SellIn < 11)
              {
                if (Items[i].Quality < 50)
                {
                  Items[i].Quality = Items[i].Quality + 1;
                }
              }

              if (Items[i].SellIn < 6)
              {
                if (Items[i].Quality < 50)
                {
                  Items[i].Quality = Items[i].Quality + 1;
                }
              }
            }
          }
        }

        if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
        {
          Items[i].SellIn = Items[i].SellIn - 1;
        }

        if (Items[i].SellIn < 0)
        {
          if (Items[i].Name != "Aged Brie")
          {
            if (Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
            {
              if (Items[i].Quality > 0)
              {
                if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                {
                  Items[i].Quality = Items[i].Quality - 1;
                }
              }
            }
            else
            {
              Items[i].Quality = Items[i].Quality - Items[i].Quality;
            }
          }
          else
          {
            if (Items[i].Quality < 50)
            {
              Items[i].Quality = Items[i].Quality + 1;
            }
          }
        }
      }
    }
  }
}
