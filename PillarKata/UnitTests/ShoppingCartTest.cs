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
        public void AddItem_AddingSoupWithNoSales_NormalSoupPrice()
        {
            //Arrange

            //Act
            var total = _sut.Add("Soup").Total;

            //Assert
            Assert.AreEqual(total, _storeItems.First(x => x.Name == "Soup").Price);
        }

        [Test]
        public void AddItem_AddingGroundBeefWithNoSalesAt1lb_NormalBeefPrice()
        {
            //Arrange

            //Act
            var total = _sut.Add("Ground Beef", 1).Total;

            //Assert
            Assert.AreEqual(total, _storeItems.First(x => x.Name == "Ground Beef").Price);
        }

        [Test]
        public void AddItem_AddingBananasWithNoSalesAt1lb_Normal1lbBananaPrice()
        {
            //Arrange

            //Act
            var total = _sut.Add("Bananas", 1).Total;

            //Assert
            Assert.AreEqual(total, _storeItems.First(x => x.Name == "Bananas").Price);
        }

        [Test]
        public void AddItem_AddingBananasWithNoSalesAt2lb_Normal2lbBananaPrice()
        {
            //Arrange

            //Act
            var total = _sut.Add("Bananas", 2).Total;

            //Assert
            Assert.AreEqual(total, _storeItems.First(x => x.Name == "Bananas").Price * 2);
        }

        [Test]
        public void AddItem_Adding2SoupWithNoSales_NormalPriceFor2Soup()
        {
            //Arrange

            //Act
            var total = _sut.Add("Soup")
                            .Add("Soup")
                            .Total;

            //Assert
            Assert.AreEqual(total, _storeItems.First(x => x.Name == "Soup").Price * 2);
        }

        [Test]
        public void AddItem_AddingSoupAnd2lbBananasWithNoSales_NormalPriceForSoupAndBanana()
        {
            //Arrange

            //Act
            var total = _sut.Add("Soup")
                            .Add("Bananas", 1)
                            .Total;

            //Assert
            Assert.AreEqual(total, _storeItems.First(x => x.Name == "Soup").Price + _storeItems.First(x => x.Name == "Bananas").Price);
        }


        [Test]
        public void AddItem_Adding2lbSoup_InvalidSoupIsByItem()
        {
            //Arrange

            //Act

            //Assert
            Assert.Throws<InvalidInputException>(() => _sut.Add("Soup", 2), "Item given must only be given 1 at a time.");
        }
    }
}
