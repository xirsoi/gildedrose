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
      foreach (Item item in Items)
      {
        if (item.Name != "Aged Brie" && item.Name != "Backstage passes to a TAFKAL80ETC concert")
        {
          if (item.Quality > 0)
          {
            if (item.Name != "Sulfuras, Hand of Ragnaros")
            {
              item.Quality = item.Quality - 1;
            }
          }
        }
        else
        {
          if (item.Quality < 50)
          {
            item.Quality = item.Quality + 1;

            if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
            {
              if (item.SellIn < 11)
              {
                if (item.Quality < 50)
                {
                  item.Quality = item.Quality + 1;
                }
              }

              if (item.SellIn < 6)
              {
                if (item.Quality < 50)
                {
                  item.Quality = item.Quality + 1;
                }
              }
            }
          }
        }

        if (item.Name != "Sulfuras, Hand of Ragnaros")
        {
          item.SellIn = item.SellIn - 1;
        }

        if (item.SellIn < 0)
        {
          if (item.Name != "Aged Brie")
          {
            if (item.Name != "Backstage passes to a TAFKAL80ETC concert")
            {
              if (item.Quality > 0)
              {
                if (item.Name != "Sulfuras, Hand of Ragnaros")
                {
                  item.Quality = item.Quality - 1;
                }
              }
            }
            else
            {
              item.Quality = item.Quality - item.Quality;
            }
          }
          else
          {
            if (item.Quality < 50)
            {
              item.Quality = item.Quality + 1;
            }
          }
        }
      }
    }
  }
}
