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
        private List<StoreItemDTO> _itemsScanned { get; set; }

        public ShoppingCart(IEnumerable<StoreItemDTO> storeItems)
        {
            Total = 0;
            _storeItems = storeItems;
            _sales = new List<SaleDTO>();
            _itemsScanned = new List<StoreItemDTO>();
        }
        
        public ShoppingCart Add(string itemName)
        {
            var itemType = _storeItems.First(x => x.Name == itemName).Type;
            if (itemType == ItemTypeEnum.ByWeight)
                throw new InvalidInputException("Item given must have a weight provided.");

            return Add(itemName, 1);
        }

        public ShoppingCart Add(string itemName, double amount)
        {
            var item = _storeItems.First(x => x.Name == itemName).Clone() as StoreItemDTO;

            if (amount != 1 && item.Type == ItemTypeEnum.ByItem)
                throw new InvalidInputException("Item given must only be given 1 at a time.");

            item.Amount = amount;
            Total += FindPrice(item);

            _itemsScanned.Add(item);

            return this;
        }

        public ShoppingCart Remove(string itemName)
        {
            var item = _itemsScanned.Where(x => x.Name == itemName).FirstOrDefault();
            _itemsScanned.Remove(item);
            RecalculateTotal();

            return this;
        }

        public void AddSale(SaleDTO saleItem)
        {
            var foundItem = _sales.FirstOrDefault(x => x.Name == saleItem.Name);
            if (foundItem != null)
                throw new InvalidInputException("Only one sale can be given at one given time");

            if (saleItem.AmountNeeded == 0)
                saleItem.AmountNeeded = 1;

            _sales.Add(saleItem);
        }

        private void RecalculateTotal()
        {
            Total = 0;
            foreach (var item in _itemsScanned)
            {
                Total += FindPrice(item);
            }
        }

        private double FindPrice(StoreItemDTO item)
        {
            var price = item.Price;
            var itemsCurrentlyInCart = _itemsScanned.Where(x => x.Name == item.Name).Count() + 1;
            var sales = _sales.FirstOrDefault(x => x.Name == item.Name);

            if (sales == null)
                return price * item.Amount;

            var salePrice = 0.0;

            if (itemsCurrentlyInCart % sales.AmountNeeded == 0 && (sales.Limit == 0 || sales.Limit <= itemsCurrentlyInCart))
                salePrice = sales.Price;

            return (price - salePrice) * item.Amount;
        }
    }
}