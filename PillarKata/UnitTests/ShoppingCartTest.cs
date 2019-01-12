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
            var total = cart.Add("Soup");

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
    }
}
