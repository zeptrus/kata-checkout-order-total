using Business.CustomExceptions;
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

        [SetUp]
        public void Init()
        {
            _storeItems = new List<StoreItemDTO>()
            {
                new StoreItemDTO() { Name = "Soup", Price = 1.89, Type = ItemTypeEnum.ByItem }
                , new StoreItemDTO() { Name = "Ground Beef", Price = 5.99, Type = ItemTypeEnum.ByWeight }
                , new StoreItemDTO() { Name = "Bananas", Price = 2.38, Type = ItemTypeEnum.ByWeight }
            };

            _sut = new ShoppingCart(_storeItems);
        }

        [Test]
        public void Add_AddingSoupWithNoSales_NormalSoupPrice()
        {
            //Arrange

            //Act
            var total = _sut.Add("Soup").Total;

            //Assert
            Assert.AreEqual(_storeItems.First(x => x.Name == "Soup").Price, total);
        }

        [Test]
        public void Add_AddingGroundBeefWithNoSalesAt1lb_NormalBeefPrice()
        {
            //Arrange

            //Act
            var total = _sut.Add("Ground Beef", 1).Total;

            //Assert
            Assert.AreEqual(_storeItems.First(x => x.Name == "Ground Beef").Price, total);
        }

        [Test]
        public void Add_AddingBananasWithNoSalesAt1lb_Normal1lbBananaPrice()
        {
            //Arrange

            //Act
            var total = _sut.Add("Bananas", 1).Total;

            //Assert
            Assert.AreEqual(_storeItems.First(x => x.Name == "Bananas").Price, total);
        }

        [Test]
        public void Add_AddingBananasWithNoSalesAt2lb_Normal2lbBananaPrice()
        {
            //Arrange

            //Act
            var total = _sut.Add("Bananas", 2).Total;

            //Assert
            Assert.AreEqual(_storeItems.First(x => x.Name == "Bananas").Price * 2, total);
        }

        [Test]
        public void Add_Adding2SoupWithNoSales_NormalPriceFor2Soup()
        {
            //Arrange

            //Act
            var total = _sut.Add("Soup")
                            .Add("Soup")
                            .Total;

            //Assert
            Assert.AreEqual(_storeItems.First(x => x.Name == "Soup").Price * 2, total);
        }

        [Test]
        public void Add_AddingSoupAnd2lbBananasWithNoSales_NormalPriceForSoupAndBanana()
        {
            //Arrange

            //Act
            var total = _sut.Add("Soup")
                            .Add("Bananas", 1)
                            .Total;

            //Assert
            Assert.AreEqual(_storeItems.First(x => x.Name == "Soup").Price + _storeItems.First(x => x.Name == "Bananas").Price, total);
        }


        [Test]
        public void Add_Adding2lbSoup_InvalidInputThrown()
        {
            //Arrange

            //Act

            //Assert
            Assert.Throws<InvalidInputException>(() => _sut.Add("Soup", 2), "Item given must only be given 1 at a time.");
        }
               
        [Test]
        public void Add_AddingSoupOnSale_SoupSoldAtReducedPrice()
        {
            //Arrange
            _sut.AddSale(new SaleDTO() { Name = "Soup", SalePrice = .20 });

            //Act
            var total = _sut.Add("Soup")
                            .Total;

            //Assert
            Assert.AreEqual(_storeItems.First(x => x.Name == "Soup").Price - .2, total);
        }

        [Test]
        public void Add_AddingBananasWithoutAWeight_InvalidInputThrown()
        {
            //Arrange

            //Act

            //Assert
            Assert.Throws<InvalidInputException>(() => _sut.Add("Bananas"), "Item given must have a weight provided.");
        }


        [Test]
        public void Add_Buy1SoupGet1SoupFree_2SoupForThePriceOf1()
        {
            //Arrange
            _sut.AddSale(new SaleDTO() { Name = "Soup", AmountNeedToSale = 2, SalePrice = 1.89 }); //Buy 1 get 1 free

            //Act
            var total = _sut.Add("Soup")
                            .Total;

            //Assert
            Assert.AreEqual(_storeItems.First(x => x.Name == "Soup").Price, total);
        }
    }
}
