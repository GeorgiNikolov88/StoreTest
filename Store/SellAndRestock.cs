using Store.Context;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Store
{
    class SellAndRestock
    {
        public void Sell()
        {
            List<int> shoppingCart = ShoppingCart();
            using (var context = new StoreContext())
            {
                var insertMoney = context.StoreMoney.First();                
                foreach (var item in shoppingCart)
                {
                    var singleItem = context.Products.Single(id => id.ProductID == item);
                    Console.WriteLine(Startup.languageInterface[38], singleItem.Brand, singleItem.InStock);
                    int ammount = InputChecker.CheckIfInt(0, singleItem.InStock);
                    singleItem.InStock -= ammount;                    
                    insertMoney.StoreCashSupply += (singleItem.Price * ammount) 
                        + ((singleItem.Price * singleItem.Overcharge) * ammount);                    
                    var Newlog = ("Date: ", DateTime.Now,
                        "Product Type: ", singleItem.Type,
                        "Brand: ", singleItem.Brand,
                        "Ammount: ", ammount,
                        "Price: ", (singleItem.Price * ammount) + ((singleItem.Price * singleItem.Overcharge) * ammount)).ToString();
                    var log = new StoreLog()
                    {
                        SalesLog = Newlog
                    };
                    context.StoreLog.Add(log);                                
                }
                context.SaveChanges();
                Console.WriteLine(Startup.languageInterface[43]);
            }
        }

        internal static int FindItem()
        {
            int itemId;
            using (var context = new StoreContext())
            {                  
                var typesInStock = context.Products.Select(t=>t.Type).ToList().Distinct();                               
                var types = context.ProductTypes.ToList();               
                List<int> available = new List<int>();                
                Console.WriteLine(Startup.languageInterface[15]);
                foreach (var item in typesInStock)
                {
                    Console.WriteLine("{0}) {1}",item, context.ProductTypes.Single(p => p.PropertyId == item).PropertyName);                    
                    available.Add(item);
                }
                int input = InputChecker.CheckIfInt();
                while (available.Contains(input) == false)
                {
                    Console.WriteLine(Startup.languageInterface[2]);
                    input = InputChecker.CheckIfInt();
                }
                var itemsFromSelectedType = context.Products.Where(id=>id.Type == input);
                available.Clear();
                Console.Clear();
                Console.WriteLine(Startup.languageInterface[47]);
                foreach (var item in itemsFromSelectedType)
                {
                    Console.WriteLine("{0}) {1}",item.ProductID,item.Brand);
                    available.Add(item.ProductID);
                }
                itemId = InputChecker.CheckIfInt();
                while (available.Contains(itemId) == false)
                {
                    Console.WriteLine(Startup.languageInterface[2]);
                    itemId = InputChecker.CheckIfInt();
                }                
            }
            return itemId;
        }

        private List<int> ShoppingCart()
        {
            int itemId;
            ConsoleKey answer = ConsoleKey.Enter;
            List<int> shoppingCart = new List<int>();
            answer = ConsoleKey.Enter;
            while (answer != ConsoleKey.Escape)
            {
                itemId = FindItem();
                if (itemId != -1)
                {
                    shoppingCart.Add(itemId);
                    Console.WriteLine(Startup.languageInterface[49]);
                    Console.WriteLine(Startup.languageInterface[0]);
                    answer = InputChecker.CheckIfEnter();
                }
                else
                {
                    Console.WriteLine(Startup.languageInterface[50]);
                }
            }
            return shoppingCart;
        }

        public void Restock()
        {
            Console.WriteLine(Startup.languageInterface[51]);
            Console.WriteLine(Startup.languageInterface[52]);
            Console.WriteLine(Startup.languageInterface[78]);
            int input = InputChecker.CheckIfInt(0, 3);
            using (var context = new StoreContext())
            {
                var moneySupply = context.StoreMoney.First();                
                var productList = context.Products.ToList();
                switch (input)
                {
                    case 1:
                        foreach (var product in productList)
                        {
                            Console.Clear();
                            Console.WriteLine(Startup.languageInterface[53]);
                            Console.WriteLine(Startup.languageInterface[54], product.Type, product.Brand, product.InStock, product.MaxStock);
                            Console.WriteLine(Startup.languageInterface[55]);
                            ConsoleKey answer = Console.ReadKey(true).Key;
                            if (answer == ConsoleKey.Enter)
                            {
                                Console.WriteLine(Startup.languageInterface[56]);
                                int order = InputChecker.CheckIfInt();
                                while (order + product.InStock > product.MaxStock || order < 0 || order * product.Price > moneySupply.StoreCashSupply)
                                {
                                    if (order + product.InStock > product.MaxStock)
                                    {
                                        Console.WriteLine(Startup.languageInterface[57], product.MaxStock);
                                    }
                                    else if (order < 0)
                                    {
                                        Console.WriteLine(Startup.languageInterface[58]);
                                    }
                                    else if (order * product.Price > moneySupply.StoreCashSupply)
                                    {
                                        Console.WriteLine(Startup.languageInterface[59]);
                                    }
                                    Console.Write(Startup.languageInterface[60]);
                                    order = InputChecker.CheckIfInt();
                                }
                                moneySupply.StoreCashSupply -= order * product.Price;
                                product.InStock = order + product.InStock;
                            }
                        }
                        context.SaveChanges();
                        break;
                    case 2:                        
                        foreach (var product in productList)
                        {
                            int ammount = product.MaxStock - product.InStock;
                            if (moneySupply.StoreCashSupply  > ammount * product.Price)
                            {
                                moneySupply.StoreCashSupply -= ammount * product.Price;
                                product.InStock = product.MaxStock;
                            }
                            else
                            {
                                Console.WriteLine(Startup.languageInterface[59]);
                            }
                        }
                        context.SaveChanges();
                        break;
                    default:
                        break;
                }
                
            }
            Console.Clear();
            Console.WriteLine(Startup.languageInterface[61]);            
        }

        public void DisplayProducts()
        {
            Console.WriteLine(Startup.languageInterface[64]);
            Console.WriteLine(Startup.languageInterface[65]);
            int input = InputChecker.CheckIfInt(0, 2);
            using (var context = new StoreContext())
            {
                switch (input)
                {
                    case 1:
                        int index = FindItem();
                        Console.Clear();
                        var selectedProduct = context.Products.Single(id => id.ProductID == index);
                        Console.WriteLine(Startup.languageInterface[66], context.ProductTypes.Find(selectedProduct.Type).PropertyName, selectedProduct.Brand);
                        Console.WriteLine(Startup.languageInterface[67], selectedProduct.InStock, selectedProduct.MaxStock);
                        Console.WriteLine(Startup.languageInterface[68], selectedProduct.Price, selectedProduct.Overcharge);

                        break;
                    case 2:
                        var products = context.Products.ToList();
                        Console.Clear();
                        foreach (var item in products)
                        {
                            Console.WriteLine(Startup.languageInterface[66], context.ProductTypes.Find(item.Type).PropertyName, item.Brand);
                            Console.WriteLine(Startup.languageInterface[67], item.InStock, item.MaxStock);
                            Console.WriteLine(Startup.languageInterface[68], item.Price, item.Overcharge);
                            Console.WriteLine();
                        }
                        break;
                    default:
                        Console.WriteLine(Startup.languageInterface[50]);
                        break;
                }
            }
            Console.WriteLine(Startup.languageInterface[0]);
            InputChecker.CheckIfEnter();            
            Console.Clear();
        }
    }
}