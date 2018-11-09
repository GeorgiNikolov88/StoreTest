using Store.Context;
using Store.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Store
{
    class SellAndRestock
    {
        //static decimal shoppingCardCost;
        public void Sell()
        {
            List<int> shoppingCart = ShoppingCart();
            using (var context = new StoreContext())
            {
                var insertMoney = context.StoreMoney.First();
                //insertMoney.StoreCashSupply = 0;
                foreach (var item in shoppingCart)
                {
                    var singleItem = context.Products.Single(id => id.ProductID == item);
                    Console.WriteLine(Startup.languageInterface[38], singleItem.Brand, singleItem.InStock);
                    int ammount = InputChecker.CheckIfInt(0, singleItem.InStock);
                    singleItem.InStock -= ammount;
                    //var insertMoney = context.StoreMoney.First();
                    insertMoney.StoreCashSupply += (singleItem.Price * ammount) + ((singleItem.Price * singleItem.Overcharge) * ammount);
                    //shoppingCardCost += shoppingCardCost + (singleItem.Price * ammount) + ((singleItem.Price * singleItem.Overcharge) * ammount);
                    var Newlog = ("Date: ", DateTime.Now,
                        "Product Type: ", Product.GetListItem(singleItem.Type),
                        "Brand: ", singleItem.Brand,
                        "Ammount: ", ammount,
                        "Price: ", (singleItem.Price * ammount) + ((singleItem.Price * singleItem.Overcharge) * ammount)).ToString();
                    var log = new StoreLog()
                    {
                        SalesLog = Newlog
                    };
                    context.StoreLog.Add(log);
                    //Console.WriteLine(Startup.languageInterface[62], shoppingCardCost);                    
                }
                context.SaveChanges();
                Console.WriteLine(Startup.languageInterface[43]);
            }
        }

        internal static int FindItem(string option)
        {
            int itemId;
            using (var context = new StoreContext())
            {
                foreach (var item in context.ProductTypes.ToList())
                {
                    Console.WriteLine("{0}){1}",item.PropertyId,item.PropertyName);
                }
                Console.Write(Startup.languageInterface[46]);
                int inpit = InputChecker.CheckIfInt(0, Product.foodType.Count);
                List<Product> selectedProductsByType = context.Products.Where(s => s.Type == inpit).ToList();
                if (selectedProductsByType.Count > 0)
                {
                    Console.WriteLine(Startup.languageInterface[47]);
                    for (int j = 0; j < selectedProductsByType.Count; j++)
                    {
                        Console.WriteLine("{0} - {1}", j, selectedProductsByType[j].Brand);
                    }
                }
                int selectItem = InputChecker.CheckIfInt(0, selectedProductsByType.Count);
                itemId = selectedProductsByType[selectItem].ProductID;
                var selectedProduct = context.Products.Single(Id => Id.ProductID == itemId);
                switch (option)
                {
                    case "edit":
                        return itemId;
                    case "sell":
                        if (selectedProduct.InStock > 0)
                        {
                            return itemId;
                        }
                        break;
                    default:
                        break;
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
                itemId = FindItem("sell");
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
                //var moneySupply.StoreCashSupply;
                var productList = context.Products.ToList();
                switch (input)
                {
                    case 1:
                        foreach (var product in productList)
                        {
                            Console.Clear();
                            Console.WriteLine(Startup.languageInterface[53]);
                            Console.WriteLine(Startup.languageInterface[54], Product.GetListItem(product.Type), product.Brand, product.InStock, product.MaxStock);
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
                        int index = FindItem("edit");

                        var selectedProduct = context.Products.Single(id => id.ProductID == index);
                        Console.WriteLine(Startup.languageInterface[66], Product.foodType[selectedProduct.Type], selectedProduct.Brand);
                        Console.WriteLine(Startup.languageInterface[67], selectedProduct.InStock, selectedProduct.MaxStock);
                        Console.WriteLine(Startup.languageInterface[68], selectedProduct.Price, selectedProduct.Overcharge);

                        break;
                    case 2:
                        var products = context.Products.ToList();
                        foreach (var item in products)
                        {
                            Console.WriteLine(Startup.languageInterface[66], Product.foodType[item.Type], item.Brand);
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
            InputChecker.CheckIfEnter();
            Console.WriteLine(Startup.languageInterface[0]);
            Console.Clear();
        }
    }
}