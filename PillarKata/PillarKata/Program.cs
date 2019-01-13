using Business.DTOs;
using Business.Enums;
using Business.Services;
using System;
using System.Collections.Generic;

namespace PillarKata
{
    class Program
    {
        private static ShoppingCart _cart;

        //just made a simple program to play with the cart.
        static void Main(string[] args)
        {
            var _storeItems = new List<StoreItemDTO>()
            {
                new StoreItemDTO() { Name = "Soup", Price = 1.89, Type = ItemTypeEnum.ByItem }
                , new StoreItemDTO() { Name = "Ground Beef", Price = 5.99, Type = ItemTypeEnum.ByWeight }
                , new StoreItemDTO() { Name = "Bananas", Price = 2.38, Type = ItemTypeEnum.ByWeight }
            };
            _cart = new ShoppingCart(_storeItems);
            while (true)
            {
                Console.WriteLine("Would you like to 'Add', 'Remove' or 'Quit'?");
                var cmd = Console.ReadLine();
                if(cmd == "Quit")
                    Environment.Exit(0);

                if(cmd != "Add" && cmd != "Remove")
                {
                    Console.WriteLine("Invalid command.");
                    continue;
                }

                Console.WriteLine($"What Item do you want to {cmd.ToLower()}? (Soup, Ground Beef, Bananas)");
                var item = Console.ReadLine();
                try
                {
                    if (item == "Soup")
                    {
                        if (cmd == "Add")
                            _cart.Add(item);
                        else
                            _cart.Remove(item);
                    }
                    else
                    {
                        var weight = 0.0;
                        Console.WriteLine($"What is the weight of {item}?");
                        var val = Console.ReadLine();
                        if (!double.TryParse(val, out weight))
                        {
                            Console.WriteLine("Invalid number given.");
                            return;
                        }
                        else
                        {
                            if (cmd == "Add")
                                _cart.Add(item, weight);
                            else
                                _cart.Remove(item, weight);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }

                Console.WriteLine($"Item {cmd}ed");
                Console.WriteLine($"Total Price: {_cart.Total}");
            }
        }
    }
}
