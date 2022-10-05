using Xunit;
using System.Collections.Generic;
using GildedRoseKata;
using System.Linq;

namespace GildedRoseTests
{
  public class GildedRoseTest
  {
    private const string BACKSTAGE_PASS_NAME = "Backstage passes to a TAFKAL80ETC concert";
    private const string SULFURAS_NAME = "Sulfuras, Hand of Ragnaros";
    private const string AGED_BRIE_NAME = "Aged Brie";

    [Fact]
    public void UpdateQuality_withZeroQuality_QualityIsNotNegative()
    {
      IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 0 } };

      GildedRose.UpdateQuality(Items);

      Assert.Equal("foo", Items[0].Name);
      Assert.Equal(0, Items[0].Quality);
      Assert.Equal(-1, Items[0].SellIn);
    }

    [Fact]
    public void UpdateQuality_withSulfuras()
    {
      IList<Item> Items = new List<Item> { new Item { Name = SULFURAS_NAME, SellIn = 12, Quality = 17 } };

      GildedRose.UpdateQuality(Items);

      Assert.Equal(12, Items.First().SellIn);
      Assert.Equal(17, Items.First().Quality);
    }

    [Fact]
    public void UpdateQuality_withAgedBrie_DoesNotSurpass50()
    {
      IList<Item> Items = new List<Item> { new Item { Name = AGED_BRIE_NAME, SellIn = 20, Quality = 49 } };

      GildedRose.UpdateQuality(Items);

      Assert.Equal(19, Items.First().SellIn);
      Assert.Equal(50, Items.First().Quality);

      GildedRose.UpdateQuality(Items);

      Assert.Equal(18, Items.First().SellIn);
      Assert.Equal(50, Items.First().Quality);
    }

    [Fact]
    public void UpdateQuality_withPastSellIn_DegradesQualityFaster()
    {
      IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = -1, Quality = 10 } };

      GildedRose.UpdateQuality(Items);

      Assert.Equal(-2, Items.First().SellIn);
      Assert.Equal(8, Items.First().Quality);
    }

    [Fact]
    public void UpdateQuality_withBackstagePass_IncreasesInQuality()
    {
      IList<Item> Items = new List<Item> { new Item { Name = BACKSTAGE_PASS_NAME, SellIn = 30, Quality = 10 } };

      GildedRose.UpdateQuality(Items);

      Assert.Equal(29, Items.First().SellIn);
      Assert.Equal(11, Items.First().Quality);
    }

    [Fact]
    public void UpdateQuality_withBackstagePassAfterConcert_LosesAllQuality()
    {
      IList<Item> Items = new List<Item> { new Item { Name = BACKSTAGE_PASS_NAME, SellIn = -1, Quality = 100 } };

      GildedRose.UpdateQuality(Items);

      Assert.Equal(-2, Items.First().SellIn);
      Assert.Equal(0, Items.First().Quality);
    }

    [Fact]
    public void UpdateQuality_withBackstagePassWithin10Days_IncreasesQualityByTwo()
    {
      IList<Item> Items = new List<Item> { new Item { Name = BACKSTAGE_PASS_NAME, SellIn = 10, Quality = 20 } };

      GildedRose.UpdateQuality(Items);

      Assert.Equal(9, Items.First().SellIn);
      Assert.Equal(22, Items.First().Quality);
    }

    [Fact]
    public void UpdateQuality_withBackstagePassWithin5Days_IncreasesQualityByThree()
    {
      IList<Item> Items = new List<Item> { new Item { Name = BACKSTAGE_PASS_NAME, SellIn = 5, Quality = 20 } };

      GildedRose.UpdateQuality(Items);

      Assert.Equal(4, Items.First().SellIn);
      Assert.Equal(23, Items.First().Quality);
    }

    [Fact]
    public void UpdateQuality_withBackstagePassWithin10Days_ValueDoesNotExceedFifty()
    {
      IList<Item> Items = new List<Item> { new Item { Name = BACKSTAGE_PASS_NAME, SellIn = 10, Quality = 49 } };

      GildedRose.UpdateQuality(Items);

      Assert.Equal(9, Items.First().SellIn);
      Assert.Equal(50, Items.First().Quality);
    }

    [Fact]
    public void UpdateQuality_withBackstagePassWithin5Days_ValueDoesNotExceedFifty()
    {
      IList<Item> Items = new List<Item> { new Item { Name = BACKSTAGE_PASS_NAME, SellIn = 5, Quality = 49 } };

      GildedRose.UpdateQuality(Items);

      Assert.Equal(4, Items.First().SellIn);
      Assert.Equal(50, Items.First().Quality);
    }

    [Fact]
    public void UpdateQuality_withMultipleItems_Succeeds()
		{
      IList<Item> Items = new List<Item>
      {
        new Item { Name = "foo", SellIn = 5, Quality = 12 },
        new Item { Name = AGED_BRIE_NAME, SellIn = 10, Quality = 49 },
        new Item { Name = SULFURAS_NAME, SellIn = 11, Quality = 80 }
      };

      GildedRose.UpdateQuality(Items);

      var foo = Items.FirstOrDefault(i => i.Name == "foo");
      var brie = Items.FirstOrDefault(i => i.Name == AGED_BRIE_NAME);
      var sulfuras = Items.FirstOrDefault(i => i.Name == SULFURAS_NAME);

      Assert.NotNull(foo);
      Assert.NotNull(brie);
      Assert.NotNull(sulfuras);

      Assert.Equal(4, foo.SellIn);
      Assert.Equal(11, foo.Quality);

      Assert.Equal(9, brie.SellIn);
      Assert.Equal(50, brie.Quality);

      Assert.Equal(11, sulfuras.SellIn);
      Assert.Equal(80, sulfuras.Quality);
    }
  }
}
