﻿using Business.CustomExceptions;
using Business.DTOs;
using Business.Enums;
using Business.Services;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    public class ShoppingCartTest
    {
        private ShoppingCart _sut;
        private List<StoreItemDTO> _storeItems;
        private const string SOUP = "Soup";//incase soup gets renamed to "Noodle soup or something"
        private const string BANANAS = "Bananas";
        private const string BEEF = "Ground Beef";
        private const string INVALIDITEM = "Invalid Item";

        [SetUp]
        public void Init()
        {
            _storeItems = new List<StoreItemDTO>()
            {
                new StoreItemDTO() { Name = SOUP, Price = 1.89, Type = ItemTypeEnum.ByItem }
                , new StoreItemDTO() { Name = BEEF, Price = 5.99, Type = ItemTypeEnum.ByWeight }
                , new StoreItemDTO() { Name = BANANAS, Price = 2.38, Type = ItemTypeEnum.ByWeight }
            };

            _sut = new ShoppingCart(_storeItems);
        }

        #region Add Tests

        [Test]
        public void Add_AddingSoupWithNoSales_NormalSoupPrice()
        {
            //Arrange

            //Act
            var total = _sut.Add(SOUP).Total;

            //Assert
            Assert.AreEqual(_storeItems.First(x => x.Name == SOUP).Price, total);
        }

        [Test]
        public void Add_AddingGroundBeefWithNoSalesAt1lb_NormalBeefPrice()
        {
            //Arrange

            //Act
            var total = _sut.Add(BEEF, 1).Total;

            //Assert
            Assert.AreEqual(_storeItems.First(x => x.Name == BEEF).Price, total);
        }

        [Test]
        public void Add_AddingBananasWithNoSalesAt1lb_Normal1lbBananaPrice()
        {
            //Arrange

            //Act
            var total = _sut.Add(BANANAS, 1).Total;

            //Assert
            Assert.AreEqual(_storeItems.First(x => x.Name == BANANAS).Price, total);
        }

        [Test]
        public void Add_AddingBananasWithNoSalesAt2lb_Normal2lbBananaPrice()
        {
            //Arrange

            //Act
            var total = _sut.Add(BANANAS, 2).Total;

            //Assert
            Assert.AreEqual(_storeItems.First(x => x.Name == BANANAS).Price * 2, total);
        }

        [Test]
        public void Add_Adding2SoupWithNoSales_NormalPriceFor2Soup()
        {
            //Arrange

            //Act
            var total = _sut.Add(SOUP)
                            .Add(SOUP)
                            .Total;

            //Assert
            Assert.AreEqual(_storeItems.First(x => x.Name == SOUP).Price * 2, total);
        }

        [Test]
        public void Add_AddingSoupAnd2lbBananasWithNoSales_NormalPriceForSoupAndBanana()
        {
            //Arrange

            //Act
            var total = _sut.Add(SOUP)
                            .Add(BANANAS, 1)
                            .Total;

            //Assert
            Assert.AreEqual(_storeItems.First(x => x.Name == SOUP).Price + _storeItems.First(x => x.Name == BANANAS).Price, total);
        }


        [Test]
        public void Add_Adding2lbSoup_InvalidInputThrown()
        {
            //Arrange

            //Act

            //Assert
            Assert.Throws<InvalidInputException>(() => _sut.Add(SOUP, 2), "Item given must only be given 1 at a time.");
        }
               
        [Test]
        public void Add_AddingSoupOnSale_SoupSoldAtReducedPrice()
        {
            //Arrange
            _sut.AddSale(new SaleDTO() { Name = SOUP, Price = .20 });

            //Act
            var total = _sut.Add(SOUP)
                            .Total;

            //Assert
            Assert.AreEqual(_storeItems.First(x => x.Name == SOUP).Price - .2, total);
        }

        [Test]
        public void Add_AddingBananasWithoutAWeight_InvalidInputThrown()
        {
            //Arrange

            //Act

            //Assert
            Assert.Throws<InvalidInputException>(() => _sut.Add(BANANAS), "Item given must have a weight provided.");
        }


        [Test]
        public void Add_Buy1SoupGet1SoupFree_2SoupForThePriceOf1()
        {
            //Arrange
            _sut.AddSale(new SaleDTO() { Name = SOUP, AmountNeeded = 2, Price = 1.89 }); //Buy 1 get 1 free

            //Act
            var total = _sut.Add(SOUP)
                            .Add(SOUP)
                            .Total;

            //Assert
            Assert.AreEqual(_storeItems.First(x => x.Name == SOUP).Price, total);
        }

        [Test]
        public void Add_Buy9SoupWithBuy3SoupFor5Limit6_6ItemsShouldBeOnSaleAndOther3AtNormalPrice()
        {
            //Arrange
            _sut.AddSale(new SaleDTO() { Name = SOUP, AmountNeeded = 3, Price = .67, Limit = 6 }); //Buy 3 soup at $5
            var expectedResult = 10.0 + _storeItems.First(x => x.Name == SOUP).Price * 3;

            //Act
            var total = _sut.Add(SOUP)
                            .Add(SOUP)
                            .Add(SOUP)
                            .Add(SOUP)
                            .Add(SOUP)
                            .Add(SOUP)
                            .Add(SOUP)
                            .Add(SOUP)
                            .Add(SOUP)
                            .Total;

            //Assert
            Assert.AreEqual(expectedResult, total, 0.01);
        }

        [Test]
        public void Add_Adding2AndHalfBannanas_PriceOf2AndHalfBananas()
        {
            //Arrange

            //Act
            var total = _sut.Add(BANANAS, 2.5)
                            .Total;

            //Assert
            Assert.AreEqual(_storeItems.First(x => x.Name == BANANAS).Price * 2.5, total);
        }

        [Test]
        public void Add_BuyBananasGetBeefHalfOff_BannanasPlusBeefAtHalfPrice()
        {
            //Arrange
            _sut.AddSale(new SaleDTO() { Name = BEEF, Price = .5, PriceType = PriceTypeEnum.Percent, PreReq = BANANAS }); //Buy bananas and get beef at equal or less value for half off
            var expectedResult = _storeItems.First(x => x.Name == BANANAS).Price * 10 + _storeItems.First(x => x.Name == BEEF).Price * .25;

            //Act
            var total = _sut.Add(BANANAS, 10)
                            .Add(BEEF, .5)
                            .Total;

            //Assert
            Assert.AreEqual(expectedResult, total);
        }

        [Test]
        public void Add_AddingAnInvalidItem_InvalidInput()
        {
            //Arrange

            //Act

            //Assert
            Assert.Throws<InvalidInputException>(() => _sut.Add(INVALIDITEM), "Item selected doesn't exist in the store's price list.");
        }

        [Test]
        public void Add_AddingAnInvalidItemWithAWeight_InvalidInput()
        {
            //Arrange

            //Act

            //Assert
            Assert.Throws<InvalidInputException>(() => _sut.Add(INVALIDITEM, 100), "Item selected doesn't exist in the store's price list.");
        }

        [Test]
        public void Add_AddingValidItemWithNegativeWeight_InvalidInput()
        {
            //Arrange

            //Act

            //Assert
            Assert.Throws<InvalidInputException>(() => _sut.Add(BEEF, -10), "Item selected has an invalid weight.");
        }

        #endregion

        #region AddSale Tests

        [Test]
        public void AddSale_HavingTwoSalesOnSoupAtOnce_InvalidInputThrown()
        {
            //Arrange
            _sut.AddSale(new SaleDTO() { Name = SOUP, AmountNeeded = 2, Price = 1.89 }); //Buy 1 get 1 free

            //Act

            //Assert
            Assert.Throws<InvalidInputException>(() => _sut.AddSale(new SaleDTO() { Name = SOUP, AmountNeeded = 3, Price = 0.95 }), "Only one sale can be given at one given time");//Buy 2 get 1 half off
        }

        [Test]
        public void AddSale_AddingInvalidItemToSale_InvalidInput()
        {
            //Arrange

            //Act

            //Assert
            Assert.Throws<InvalidInputException>(() => _sut.AddSale(new SaleDTO() { Name = INVALIDITEM }), "Item selected is an invalid item to put on sale.");
        }

        [Test]
        public void AddSale_AddingValidItemToSaleWithInvalidPreReqItem_InvalidInput()
        {
            //Arrange

            //Act

            //Assert
            Assert.Throws<InvalidInputException>(() => _sut.AddSale(new SaleDTO() { Name = BEEF, PreReq = INVALIDITEM }), "Prerequisite item is an invalid item.");
        }


        [Test]
        public void AddSale_AmountNeededIsHigherThenLimit_InvalidInput()
        {
            //Arrange

            //Act

            //Assert
            Assert.Throws<InvalidInputException>(() => _sut.AddSale(new SaleDTO() { Name = BEEF, AmountNeeded = 10, Limit = 5 }), "Amount needed must be less then max items on sale.");
        }

        #endregion

        #region Remove Tests

        [Test]
        public void Remove_RemoveSoupFrom2Soup_PriceOf1Soup()
        {
            //Arrange

            //Act
            var total = _sut.Add(SOUP)
                            .Add(SOUP)
                            .Remove(SOUP)
                            .Total;

            //Assert
            Assert.AreEqual(_storeItems.First(x => x.Name == SOUP).Price, total);
        }


        [Test]
        public void Remove_RemoveBananas_PriceWithoutBananas()
        {
            //Arrange

            //Act
            var total = _sut.Add(BANANAS, 2.5)
                            .Add(BANANAS, 2)
                            .Remove(BANANAS, 2)
                            .Total;

            //Assert
            Assert.AreEqual(_storeItems.First(x => x.Name == BANANAS).Price * 2.5, total);
        }

        [Test]
        public void Remove_Buy1SoupGet1SoupFreeButOnlyBuy1_PriceOfOneSoup()
        {
            //Arrange
            _sut.AddSale(new SaleDTO() { Name = SOUP, AmountNeeded = 3, Price = .95 }); //Buy 2 get 1 half off

            //Act
            var total = _sut.Add(SOUP)
                            .Add(SOUP)
                            .Remove(SOUP)
                            .Total;

            //Assert
            Assert.AreEqual(_storeItems.First(x => x.Name == SOUP).Price, total);
        }

        [Test]
        public void Remove_RemovingBeefThatWasntAdded_InvalidInput()
        {
            //Arrange

            //Act
            
            //Assert
            Assert.Throws<InvalidInputException>(() => _sut.Add(BEEF, 10).Remove(BEEF, 7), "Item selected to be removed isn't in the cart.");//Buy 2 get 1 half off

        }

        #endregion
    }
}
