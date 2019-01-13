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
            var itemType = _storeItems.FirstOrDefault(x => x.Name == itemName)?.Type;

            if (itemType == null)
                throw new InvalidInputException("Item selected doesn't exist in the store's price list.");

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
            return Remove(itemName, 1);
        }

        public ShoppingCart Remove(string itemName, double amount)
        {
            var item = _itemsScanned.Where(x => x.Name == itemName && x.Amount == amount).FirstOrDefault();

            if (item == null)
                throw new InvalidInputException("Item selected to be removed isn't in the cart.");

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
            var oldItemsScanned = _itemsScanned.ToList();
            _itemsScanned.Clear();
            foreach (var item in oldItemsScanned)
            {
                Total += FindPrice(item);
                _itemsScanned.Add(item);
            }
        }

        private double FindPrice(StoreItemDTO item)
        {
            var price = item.Price;
            var itemsCurrentlyInCart = _itemsScanned.Where(x => x.Name == item.Name).Count() + 1;
            var sale = _sales.FirstOrDefault(x => x.Name == item.Name);

            if (sale == null)
                return price * item.Amount;

            var salePrice = 0.0;

            if (itemsCurrentlyInCart % sale.AmountNeeded == 0 && (sale.Limit == 0 || sale.Limit <= itemsCurrentlyInCart))
                salePrice = sale.Price;

            if (sale.PriceType == PriceTypeEnum.Percent)
                salePrice = price * salePrice;

            var expectedPrice = (price - salePrice) * item.Amount;

            if (sale.PreReq != null)
            {
                var preReqPrice = _itemsScanned.Where(x => x.Name == sale.PreReq).Sum(x => x.Price * x.Amount);
                if (preReqPrice < expectedPrice)
                    return price * item.Amount;
            }

            return expectedPrice;
        }
    }
}