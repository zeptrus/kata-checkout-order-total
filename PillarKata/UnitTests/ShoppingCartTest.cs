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
    }
}
