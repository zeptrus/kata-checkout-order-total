using Business.CustomExceptions;
using Business.DTOs;
using Business.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Services
{
    public class ShoppingCart
    {
        public double Total { get; private set; }
        private IEnumerable<StoreItemDTO> _storeItems { get; set; }
        private List<SaleDTO> _sales { get; set; }

        public ShoppingCart(IEnumerable<StoreItemDTO> storeItems)
        {
            Total = 0;
            _storeItems = storeItems;
            _sales = new List<SaleDTO>();
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

            var price = item.Price;
            var salePrice = _sales.FirstOrDefault(x => x.Name == itemName)?.SalePrice ?? 0;
            Total += (price - salePrice) * amount;

            return this;
        }

        public void AddSale(SaleDTO saleItem)
        {
            _sales.Add(saleItem);
        }
    }
}