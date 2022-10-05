﻿using Xunit;
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
      var sut = new GildedRose(Items);
      sut.UpdateQuality();
      Assert.Equal("foo", Items[0].Name);
      Assert.Equal(0, Items[0].Quality);
      Assert.Equal(-1, Items[0].SellIn);
    }

    [Fact]
    public void UpdateQuality_withSulfuras()
    {
      IList<Item> Items = new List<Item> { new Item { Name = SULFURAS_NAME, SellIn = 12, Quality = 17 } };
      var sut = new GildedRose(Items);
      sut.UpdateQuality();
      Assert.Equal(12, Items.First().SellIn);
      Assert.Equal(80, Items.First().Quality);
    }

    [Fact]
    public void UpdateQuality_withAgedBrie_DoesNotSurpass50()
		{
      IList<Item> Items = new List<Item> { new Item { Name = AGED_BRIE_NAME, SellIn = 20, Quality = 49 } };
      var sut = new GildedRose(Items);
      sut.UpdateQuality();
      Assert.Equal(19, Items.First().SellIn);
      Assert.Equal(50, Items.First().Quality);
      sut.UpdateQuality();
      Assert.Equal(18, Items.First().SellIn);
      Assert.Equal(50, Items.First().Quality);
		}

		[Fact]
		public void UpdateQuality_withQualityOver50_ReducesQualityTo50()
		{
			IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 10, Quality = 100 } };
			var sut = new GildedRose(Items);
			sut.UpdateQuality();
			Assert.Equal(50, Items[0].Quality);
			Assert.Equal(9, Items[0].SellIn);
		}

		[Fact]
    public void UpdateQuality_withPastSellIn_DegradesQualityFaster()
		{
      IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = -1, Quality = 10 } };
      var sut = new GildedRose(Items);
      sut.UpdateQuality();
      Assert.Equal(-2, Items.First().SellIn);
      Assert.Equal(8, Items.First().Quality);
		}

    [Fact]
    public void UpdateQuality_withBackstagePass_IncreasesInQuality()
		{
      IList<Item> Items = new List<Item> { new Item { Name = BACKSTAGE_PASS_NAME, SellIn = 30, Quality = 10 } };
      var sut = new GildedRose(Items);
      sut.UpdateQuality();
      Assert.Equal(29, Items.First().SellIn);
      Assert.Equal(11, Items.First().Quality);
		}

    [Fact]
    public void UpdateQuality_withBackstagePassAfterConcert_LosesAllQuality()
		{
      IList<Item> Items = new List<Item> { new Item { Name = BACKSTAGE_PASS_NAME, SellIn = -1, Quality = 100 } };
      var sut = new GildedRose(Items);
      sut.UpdateQuality();
      Assert.Equal(-2, Items.First().SellIn);
      Assert.Equal(0, Items.First().Quality);
		}
  }
}
