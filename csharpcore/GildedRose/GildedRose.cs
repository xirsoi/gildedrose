using System;
using System.Collections.Generic;
using System.Linq;

namespace GildedRoseKata
{
  public class GildedRose
  {
    static IList<Item> Items;
    public const string BACKSTAGE_PASS_NAME = "Backstage passes to a TAFKAL80ETC concert";
    public const string SULFURAS_NAME = "Sulfuras, Hand of Ragnaros";
    public const string AGED_BRIE_NAME = "Aged Brie";
    private static List<string> specialProductNames = new() { BACKSTAGE_PASS_NAME, SULFURAS_NAME, AGED_BRIE_NAME };

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
      foreach (Item i in Items.Where(i => i.Name == BACKSTAGE_PASS_NAME))
        updateBackstagePass(i);

      foreach (Item i in Items.Where(i => i.Name == AGED_BRIE_NAME))
        updateBrie(i);

      foreach (Item i in Items.Where(i => i.Name == SULFURAS_NAME))
        updateSulfuras(i);

      foreach (Item i in Items.Where(i => !specialProductNames.Contains(i.Name)))
        updateOther(i);
    }

    private static void updateSulfuras(Item sulfuras)
    {
      // currently a nop, but having it so any changes are easier in future
    }

    private static void updateBrie(Item brie)
		{
      if (brie.SellIn >= 0)
        brie.Quality++;
      else
			{
        brie.Quality -= 2;
			}

      clampQuality(brie);
      brie.SellIn--;
		}

    private static void updateBackstagePass(Item pass)
		{
      pass.Quality++;
      if (pass.SellIn <= 10)
        pass.Quality++;
      if (pass.SellIn <= 5)
        pass.Quality++;
      if (pass.SellIn < 0)
        pass.Quality = 0;

      clampQuality(pass);
      pass.SellIn--;
		}

    private static void updateOther(Item item)
		{
      if (item.SellIn >= 0) item.Quality--;
      else item.Quality -= 2;

      clampQuality(item);
      item.SellIn--;
    }

    private static void clampQuality(Item item)
		{
      item.Quality = Math.Min(item.Quality, 50);
      item.Quality = Math.Max(item.Quality, 0);
		}
  }
}
