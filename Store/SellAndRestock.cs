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
        static decimal shoppingCardCost;
        public void Sell(List<Product> list)
        {
            List<int> shoppingCart = ShoppingCart();
            using (var context = new StoreContext())
            {
                foreach (var item in shoppingCart)
                {
                    var singleItem = context.Products.Single(id => id.ProductID == item);                    
                    Console.WriteLine(Startup.languageInterface[38], singleItem.Brand, singleItem.InStock);
                    int ammount = InputChecker.CheckIfInt(0,singleItem.InStock);
                    singleItem.InStock -= ammount;
                    var sum = context.StoreMoney.First();
                    
                    Console.WriteLine(sum);
                   
                    shoppingCardCost = shoppingCardCost + (singleItem.Price * ammount) + ((singleItem.Price * singleItem.Overcharge) * ammount);
                   
                    Console.WriteLine(shoppingCardCost);
                    //sum.StoreCashSupply = Startup.shopCash + (singleItem.Price * ammount) + ((singleItem.Price * singleItem.Overcharge) * ammount);
                    //Console.WriteLine(Startup.languageInterface[41], Product.GetListItem(singleItem.Type), singleItem.Brand, singleItem.InStock);

                    
                    var Newlog = (DateTime.Now,Product.GetListItem(singleItem.Type),singleItem.Brand, ammount, (singleItem.Price * ammount) + ((singleItem.Price * singleItem.Overcharge) * ammount)).ToString();
                    Console.WriteLine(Newlog);
                    Console.ReadLine();
                    var log = new StoreLog()
                    {      
                        SalesLog = Newlog
                    };
                    context.StoreLog.Add(log);                    
                    // context.Add<StoreLog>(log);
                    //////context.StoreLog.Add("aaaa");
                    ////using (StreamWriter sw = new StreamWriter("files/log.txt", true))
                    ////{
                    ////    sw.WriteLine(Startup.languageInterface[42], DateTime.Now, Product.GetListItem(singleItem.Type), singleItem.Brand, ammount, (singleItem.Price * ammount) + ((singleItem.Price * singleItem.Overcharge) * ammount));
                    ////}
                    ////Console.WriteLine(Startup.languageInterface[62], shoppingCardCost);                  
                    context.SaveChanges();
                    Console.WriteLine(Startup.languageInterface[43]);
                }
            }
            
            //return list;
        }

        internal static int FindItem(string option)
        {
            int itemId;
            using (var context = new StoreContext())
            {
                foreach (var item in Product.foodType)
                {
                    Console.WriteLine(item);
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
            List <int> shoppingCart = new List<int>();
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

        public List<Product> Restock(List<Product> list)
        {
            Console.WriteLine(Startup.languageInterface[51]);
            Console.WriteLine(Startup.languageInterface[52]);
            Console.WriteLine(Startup.languageInterface[78]);
            //int.TryParse(Console.ReadLine(), out int input);
            int input = InputChecker.CheckIfInt(3);
            switch (input)
            {
                case 1:
                    foreach (var product in list)
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
                            while (order + product.InStock > product.MaxStock || order < 0 || order * product.Price > Startup.shopCash)
                            {
                                if (order + product.InStock > product.MaxStock)
                                {
                                    Console.WriteLine(Startup.languageInterface[57], product.MaxStock);
                                }
                                else if (order<0)
                                {
                                    Console.WriteLine(Startup.languageInterface[58]);
                                }
                                else if (order * product.Price > Startup.shopCash)
                                {
                                    Console.WriteLine(Startup.languageInterface[59]);
                                }
                                Console.Write( Startup.languageInterface[60]);
                                order = InputChecker.CheckIfInt();
                            }
                            Startup.shopCash = Startup.shopCash - order * product.Price;
                            product.InStock = order + product.InStock;
                        }
                    }
                    break;
                case 2:
                    foreach (var product in list)
                    {
                        int ammount = product.MaxStock - product.InStock;
                        if (Startup.shopCash > ammount * product.Price)
                        {
                            Startup.shopCash = Startup.shopCash - ammount * product.Price;
                            product.InStock = product.MaxStock;
                        }
                        else
                        {
                            Console.WriteLine(Startup.languageInterface[59]);
                        }    
                    }
                    break;
                default: 
                    
                    break;
            }
            Console.Clear();
            Console.WriteLine(Startup.languageInterface[61]);
            return list;
        }

        public void DisplayProducts(List<Product> list)
        {
            Console.WriteLine(Startup.languageInterface[64]);
            Console.WriteLine(Startup.languageInterface[65]);
            int input = InputChecker.CheckIfInt();
            while (input>2 || input<0)
            {
                input = InputChecker.CheckIfInt();
            }            
            switch (input)
            {
                case 1:
                    int index = FindItem("edit");                    
                    Console.WriteLine(Startup.languageInterface[66], Product.foodType[list[index].Type], list[index].Brand);
                    Console.WriteLine(Startup.languageInterface[67], list[index].InStock, list[index].MaxStock);
                    Console.WriteLine(Startup.languageInterface[68], list[index].Price, list[index].Overcharge);
                    Console.WriteLine();
                    break;
                case 2:
                    foreach (var item in list)
                    {
                        Console.WriteLine(Startup.languageInterface[66] , Product.foodType[item.Type], item.Brand);
                        Console.WriteLine(Startup.languageInterface[67] , item.InStock, item.MaxStock);
                        Console.WriteLine(Startup.languageInterface[68] , item.Price, item.Overcharge);
                        Console.WriteLine();
                    }
                    break;
                default:
                    Console.WriteLine(Startup.languageInterface[50]);
                    break;
            }
            InputChecker.CheckIfEnter();
            Console.WriteLine();
            Console.WriteLine(Startup.languageInterface[0]);
            Console.Clear();
        }
    }
}