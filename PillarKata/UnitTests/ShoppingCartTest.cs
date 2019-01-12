using Business;
using NUnit.Framework;

namespace UnitTests
{
    public class ShoppingCartTest
    {
        [Test]
        public void AddItem_AddingSoupWithNoSales_NormalSoupPrice()
        {
            //Arrange
            var cart = new ShoppingCart();

            //Act
            var total = cart.Add("Soup").Total;

            //Assert
            Assert.AreEqual(total, 1.89);
        }

        [Test]
        public void AddItem_AddingGroundBeefWithNoSalesAt1lb_NormalBeefPrice()
        {
            //Arrange
            var cart = new ShoppingCart();

            //Act
            var total = cart.Add("Ground Beef", 1).Total;

            //Assert
            Assert.AreEqual(total, 5.99);
        }

        [Test]
        public void AddItem_AddingBananasWithNoSalesAt1lb_Normal1lbBananaPrice()
        {
            //Arrange
            var cart = new ShoppingCart();

            //Act
            var total = cart.Add("Bananas", 1).Total;

            //Assert
            Assert.AreEqual(total, 2.38);
        }

        [Test]
        public void AddItem_AddingBananasWithNoSalesAt2lb_Normal2lbBananaPrice()
        {
            //Arrange
            var cart = new ShoppingCart();

            //Act
            var total = cart.Add("Bananas", 2).Total;

            //Assert
            Assert.AreEqual(total, 2.38 * 2);
        }

        [Test]
        public void AddItem_Adding2SoupWithNoSales_NormalPriceFor2Soup()
        {
            //Arrange
            var cart = new ShoppingCart();

            //Act
            var total = cart.Add("Soup")
                            .Add("Soup")
                            .Total;

            //Assert
            Assert.AreEqual(total, 1.89 * 2);
        }

        [Test]
        public void AddItem_AddingSoupAnd2lbBananasWithNoSales_NormalPriceForSoupAndBanana()
        {
            //Arrange
            var cart = new ShoppingCart();

            //Act
            var total = cart.Add("Soup")
                            .Add("Bananas", 1)
                            .Total;

            //Assert
            Assert.AreEqual(total, 1.89 + 2.38);
        }
    }
}
