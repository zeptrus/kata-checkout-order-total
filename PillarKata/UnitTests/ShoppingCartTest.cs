using Business;
using NUnit.Framework;

namespace UnitTests
{
    public class ShoppingCartTest
    {
        [Test]
        public void AddItem_AddingSoupWithNoSales_NormalPrice()
        {
            //Arrange
            var cart = new ShoppingCart();

            //Act
            var total = cart.Add("Soup").Total;

            //Assert
            Assert.AreEqual(total, 1.89);
        }

        [Test]
        public void AddItem_AddingGroundBeefWithNoSalesAt1lb_NormalPrice()
        {
            //Arrange
            var cart = new ShoppingCart();

            //Act
            var total = cart.Add("Ground Beef", 1);

            //Assert
            Assert.AreEqual(total, 5.99);
        }

        [Test]
        public void AddItem_AddingBananasWithNoSalesAt1lb_NormalPrice()
        {
            //Arrange
            var cart = new ShoppingCart();

            //Act
            var total = cart.Add("Bananas", 1);

            //Assert
            Assert.AreEqual(total, 2.38);
        }

        [Test]
        public void AddItem_AddingBananasWithNoSalesAt2lb_NormalPrice()
        {
            //Arrange
            var cart = new ShoppingCart();

            //Act
            var total = cart.Add("Bananas", 2);

            //Assert
            Assert.AreEqual(total, 4.76);
        }

        [Test]
        public void AddItem_Adding2SoupWithNoSales_NormalPrice()
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
    }
}
