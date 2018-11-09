﻿using Microsoft.EntityFrameworkCore;
using Store.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Store
{
    class Startup
    {
        public static decimal shopCash = 0m;
        public static List<string> languageInterface =  Interfaces.SelectInterface();
        static void Main(string[] args)
        {
            using (var context = new StoreContext())
            {
                Product.foodType = context.ProductTypes.Select(item => item.PropertyName).ToList();
            }
            //ExportAndInport creator = new ExportAndInport();
            CRUDProduct newProduct = new CRUDProduct();
            //List<Product> list = creator.ImportStoreDataFromFiles();           
            SellAndRestock transaction = new SellAndRestock();
            Console.Clear();
            ConsoleKey cont = ConsoleKey.Enter;
            while (cont == ConsoleKey.Enter)
            {
                Console.Clear();
                Console.WriteLine(languageInterface[3]);
                Console.WriteLine(languageInterface[4]);
                Console.WriteLine(languageInterface[5]);
                Console.WriteLine(languageInterface[70]);
                Console.WriteLine(languageInterface[63]);
                Console.WriteLine(languageInterface[9]);
                Console.WriteLine(languageInterface[10], 5);
                Console.Write(languageInterface[1]);                
                int input = InputChecker.CheckIfInt(5);
                switch (input)
                {
                    case 1:
                        Console.Clear();
                        ConsoleKey answer = ConsoleKey.Enter;
                        while (answer != ConsoleKey.Escape)
                        {
                            Console.Clear();
                            transaction.Sell();
                            Console.WriteLine(languageInterface[0]);
                            answer = InputChecker.CheckIfEnter();
                            Console.Clear();
                        }
                        break;
                    case 2:
                        //Избиране на опция от менюто за управление на инвентара
                        Console.Clear();
                        Console.WriteLine(languageInterface[6]);                        
                        Console.WriteLine(languageInterface[8]);
                        Console.WriteLine(languageInterface[7]);
                        Console.WriteLine(languageInterface[74]);
                        Console.WriteLine(languageInterface[75]);
                        Console.WriteLine(languageInterface[76]);
                        Console.WriteLine(languageInterface[10], 7);
                        input = InputChecker.CheckIfInt(7);
                        switch (input)
                        {
                            case 1:
                                Console.Clear();
                                newProduct.CreateNewProduct(languageInterface);
                                //list.Add();
                                break;
                            case 2:
                                Console.Clear();
                                newProduct.DeleteItem();

                                break;
                            case 3:
                                Console.Clear();
                                newProduct.EditProduct();
                                break;
                            case 4:
                                Console.Clear();
                                Product.AddItemType();
                                break;
                            case 5:
                                Console.Clear();
                                Product.RemoveItemType();
                                break;
                            case 6:
                                Console.Clear();
                                Product.EditProductType();
                                break;
                            default:
                                break;
                        }
                        break;                    
                    case 3:
                        Console.Clear();
                        transaction.DisplayProducts();
                        break;
                    case 4:
                        Console.Clear();
                        transaction.Restock();
                        
                        break;
                    case 5:
                        Console.Clear();
                        //ExportAndInport.ExportStoreDataToFiles(list);
                        return;                        
                    default:
                        break;
                }
            }
        }
    }
}


//            using (var context = new StoreContext())
//            {
//                foreach (var item in Product.foodType)
//                {
//                    Console.WriteLine(item);
//                }
//                Console.Write(Startup.languageInterface[46]);
//                int inpit = InputChecker.CheckIfInt(0, Product.foodType.Count);
//List<Product> selectedProductsByType = context.Products.Where(s => s.Type == inpit).ToList();
//string option = "sell";
//                if (selectedProductsByType.Count > 0)
//                {
//                    Console.WriteLine(Startup.languageInterface[47]);
//                    for (int j = 0; j<selectedProductsByType.Count; j++)
//                    {
//                        Console.WriteLine("{0} - {1}", j, selectedProductsByType[j].Brand);
//                    }
//                }
//                int selectItem = InputChecker.CheckIfInt(0, selectedProductsByType.Count);
//int itemId = selectedProductsByType[selectItem].ProductID;
//var selectedProduct = context.Products.Single(Id => Id.ProductID == itemId);
               
//                switch (option)
//                {
//                    case "edit":
//                        Console.WriteLine(selectedProduct.Brand);
//                        break;
//                    case "sell":
//                        if (selectedProduct.InStock > 0)
//                        {
//                            Console.WriteLine(selectedProduct.InStock);
//                        }
//                        break;
//                    default:
//                        break;
//                }
//            }







//                Console.ReadLine();