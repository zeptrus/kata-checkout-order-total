using Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Services
{
    public class ShoppingCart
    {
        public double Total { get; private set; }
        private IEnumerable<StoreItemDTO> _storeItems { get; set; }

        public ShoppingCart(IEnumerable<StoreItemDTO> storeItems)
        {
            Total = 0;
            _storeItems = storeItems;
        }
        
        public ShoppingCart Add(string itemName)
        {
            return Add(itemName, 1);
        }

        public ShoppingCart Add(string itemName, int weight)
        {
            Total += _storeItems.First(x => x.Name == itemName).Price * weight;

            return this;
        }
    }
}