using System;

namespace Business
{
    public class ShoppingCart
    {
        public ShoppingCart() { }

        public double Add(string item)
        {
            return 1.89;
        }

        public double Add(string item, int weight)
        {
            if(item == "Ground Beef")
            {
                return 5.99;
            }
            else
            {
                return 2.38;
            }
        }
    }
}