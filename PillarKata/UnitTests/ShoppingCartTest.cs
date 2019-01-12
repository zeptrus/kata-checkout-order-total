using Business;
using NUnit.Framework;

namespace UnitTests
{
    public class ShoppingCartTest
    {
        private ShoppingCart _sut;

        [SetUp]
        public void Init()
        {
            _sut = new ShoppingCart();
        }

        [Test]
        public void AddItem_AddingSoupWithNoSales_NormalSoupPrice()
        {
            //Arrange

            //Act
            var total = _sut.Add("Soup").Total;

            //Assert
            Assert.AreEqual(total, 1.89);
        }

        [Test]
        public void AddItem_AddingGroundBeefWithNoSalesAt1lb_NormalBeefPrice()
        {
            //Arrange

            //Act
            var total = _sut.Add("Ground Beef", 1).Total;

            //Assert
            Assert.AreEqual(total, 5.99);
        }

        [Test]
        public void AddItem_AddingBananasWithNoSalesAt1lb_Normal1lbBananaPrice()
        {
            //Arrange

            //Act
            var total = _sut.Add("Bananas", 1).Total;

            //Assert
            Assert.AreEqual(total, 2.38);
        }

        [Test]
        public void AddItem_AddingBananasWithNoSalesAt2lb_Normal2lbBananaPrice()
        {
            //Arrange

            //Act
            var total = _sut.Add("Bananas", 2).Total;

            //Assert
            Assert.AreEqual(total, 2.38 * 2);
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
            Assert.AreEqual(total, 1.89 * 2);
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
            Assert.AreEqual(total, 1.89 + 2.38);
        }
    }
}
