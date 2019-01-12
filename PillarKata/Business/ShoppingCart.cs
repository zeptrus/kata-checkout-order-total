using System;

namespace Business
{
    public class ShoppingCart
    {
        public double Total { get; private set; }

        public ShoppingCart()
        {
            Total = 0;
        }

        public ShoppingCart Add(string item)
        {
            Total += 1.89;
            return this;
        }

        public ShoppingCart Add(string item, int weight)
        {
            var price = 0.0;
            if(item == "Ground Beef")
            {
                price = 5.99;
            }
            else
            {
                price = 2.38;
            }

            Total += price * weight;
            return this;
        }
    }
}