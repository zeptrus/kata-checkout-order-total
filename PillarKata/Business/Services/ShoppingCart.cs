using Business.CustomExceptions;
using Business.DTOs;
using Business.Enums;
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

        public ShoppingCart Add(string itemName, int amount)
        {
            var item = _storeItems.First(x => x.Name == itemName);

            if (amount != 1 && item.Type == ItemTypeEnum.ByItem)
                throw new InvalidInputException("Item given must only be given 1 at a time.");

            Total += item.Price * amount;

            return this;
        }
    }
}