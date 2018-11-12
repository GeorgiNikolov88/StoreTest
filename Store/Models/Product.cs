using Store.Context;
using Store.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Store
{
    class Product
    {
        string _brand;
        decimal _price;
        int _inStock;
        int _type;
        //public static List<string> foodType = new List<string> { "bread", "vegeable", "meat", "water", "chocolate" };
        public static List<string> foodType = new List<string>();
        int _maxStock;
        decimal _overcharge;
        private const string password = "1";
        public int ProductID { get; set; }
        public string Brand
        {
            get { return _brand; }
            set { _brand = value; }
        }
        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }
        public int InStock
        {
            get { return _inStock; }
            set { _inStock = value; }
        }
        public int Type
        {
            get { return _type; }
            set { _type = value; }
        }
        public int MaxStock
        {
            get { return _maxStock; }
            set
            {
                //Console.Write("Enter password: ");
                //ConsoleKeyInfo inf;
                //StringBuilder input = new StringBuilder();
                //inf = Console.ReadKey(true);
                //while (inf.Key != ConsoleKey.Enter)
                //{
                //    input.Append(inf.KeyChar);
                //    inf = Console.ReadKey(true);
                //}
                //string password = "1";
                //if (input.ToString() == password)
                //{
                _maxStock = value;
                //}
                //Console.WriteLine();
            }
        }
        public decimal Overcharge
        {
            get { return _overcharge; }
            set { _overcharge = value; }
        }
        public static void AddItemType()
        {
            Console.Clear();
            Console.WriteLine(Startup.languageInterface[19]);
            string insertPassword = Console.ReadLine();
            ConsoleKey answer = ConsoleKey.Enter;
            while (answer != ConsoleKey.Escape)
            {

                if (insertPassword == "1")
                {
                    Console.WriteLine(Startup.languageInterface[21]);
                    var addition = CRUDProduct.CheckIfTypeExists(Console.ReadLine());
                    if (addition != string.Empty)
                    {
                        using (var context = new StoreContext())
                        {
                            var newType = new ProductTypes()
                            {
                                PropertyName = addition
                            };
                            context.ProductTypes.Add(newType);
                            context.SaveChanges();
                        }                        
                        answer = ConsoleKey.Escape;                        
                    }
                    else
                    {
                        return;
                    }                    
                }
                else
                {
                    Console.WriteLine(Startup.languageInterface[18]);
                    Console.WriteLine(Startup.languageInterface[0]);
                    answer = InputChecker.CheckIfEnter();
                    Console.Write(Startup.languageInterface[19]);
                    insertPassword = Console.ReadLine();
                }
            }
        }

        public static void RemoveItemType()
        {
            Console.Clear();
            Console.WriteLine(Startup.languageInterface[19]);
            string insertPassword = Console.ReadLine();
            ConsoleKey answer = ConsoleKey.Enter;
            while (answer != ConsoleKey.Escape)
            {
                if (insertPassword == "1")
                {
                    using (var context = new StoreContext())
                    {
                        var productTypes = context.ProductTypes.ToList();
                        foreach (var item in productTypes)
                        {
                            Console.WriteLine("{0}) {1}", item.PropertyId, item.PropertyName);
                        }
                        //Check if the type exists
                        List<int> typeIds = new List<int>();
                        foreach (var item in productTypes)
                        {
                            typeIds.Add(item.PropertyId);
                        }
                        Console.WriteLine(Startup.languageInterface[69]);
                        int itemToRemove = InputChecker.CheckIfInt(productTypes.Count);
                        while (!typeIds.Contains(itemToRemove))
                        {
                            Console.WriteLine(Startup.languageInterface[36]);
                            itemToRemove = InputChecker.CheckIfInt(1, context.ProductTypes.Last().PropertyId);
                        }
                        //Deleting the selected type
                        Console.WriteLine(Startup.languageInterface[23], context.ProductTypes.Single(id=>id.PropertyId == itemToRemove).PropertyName);
                        answer = InputChecker.CheckIfEnter();
                        if (answer == ConsoleKey.Enter)
                        {
                            var typeToRemove = context.ProductTypes.Single(id => id.PropertyId == itemToRemove);
                            context.ProductTypes.Remove(typeToRemove);
                            context.SaveChanges();                            
                            answer = ConsoleKey.Escape;
                        }
                    }
                }
                else
                {
                    Console.WriteLine(Startup.languageInterface[18]);
                    Console.WriteLine(Startup.languageInterface[0]);
                    answer = InputChecker.CheckIfEnter();
                    Console.Write(Startup.languageInterface[19]);
                    insertPassword = Console.ReadLine();
                }
            }
        }

        public static void EditProductType()
        {
            Console.WriteLine(Startup.languageInterface[19]);
            string insertPassword = Console.ReadLine();
            ConsoleKey answer = ConsoleKey.Enter;
            while (answer != ConsoleKey.Escape)
            {
                if (insertPassword == "1")
                {
                    using (var context = new StoreContext())
                    {
                        var productTypes = context.ProductTypes.ToList();
                        Console.Clear();
                        foreach (var item in productTypes)
                        {
                            Console.WriteLine("{0}) {1}", item.PropertyId, item.PropertyName);
                        } 
                        Console.WriteLine(Startup.languageInterface[71]);
                        //Check if the type exists
                        List<int> typeIds = new List<int>();
                        foreach (var item in productTypes)
                        {
                            typeIds.Add(item.PropertyId);
                        }
                        int itemToEdit = InputChecker.CheckIfInt(1, context.ProductTypes.Last().PropertyId);
                        while (!typeIds.Contains(itemToEdit))
                        {
                            Console.WriteLine(Startup.languageInterface[36]);
                            itemToEdit = InputChecker.CheckIfInt(1, context.ProductTypes.Last().PropertyId);
                        }
                        //change the name of the type
                        Console.WriteLine("item to edit{0}", context.ProductTypes.Single(id=>id.PropertyId == itemToEdit).PropertyName);
                        Console.WriteLine(Startup.languageInterface[21]);
                        string newName = CRUDProduct.CheckIfTypeExists(Console.ReadLine());
                        if (newName != string.Empty)
                        {
                            //Console.WriteLine(itemToEdit);
                           // Console.WriteLine(context.ProductTypes.Single(id => id.PropertyId == itemToEdit).PropertyName);
                            //Console.WriteLine(Startup.languageInterface[73], context.ProductTypes.Single(id => id.PropertyId == itemToEdit).PropertyName);
                            Console.WriteLine(Startup.languageInterface[0]);
                            if (InputChecker.CheckIfEnter() == ConsoleKey.Enter)
                            {

                                var typeToEdit = context.ProductTypes.Single(id => id.PropertyId == itemToEdit);
                                //Console.WriteLine(typeToEdit.PropertyName);
                                //Console.ReadLine();
                                typeToEdit.PropertyName = newName;
                                context.SaveChanges();


                            }
                            else
                            {
                                Console.WriteLine(Startup.languageInterface[34]);
                            }
                            answer = ConsoleKey.Escape;
                        }
                    }
                }
                else
                {
                    Console.WriteLine(Startup.languageInterface[18]);
                    Console.WriteLine(Startup.languageInterface[0]);
                    answer = InputChecker.CheckIfEnter();
                    Console.Write(Startup.languageInterface[19]);
                    insertPassword = Console.ReadLine();
                }
            }
        }

        //public static string GetListItem(int index)
        //{
        //    return foodType[index];
        //}

        public Product(string brand, decimal price, int inStock, int type, int maxStock, decimal overcharge)
        {
            this.Brand = brand;
            this.Price = price;
            this.InStock = inStock;
            this.Type = type;
            this.MaxStock = maxStock;
            this.Overcharge = overcharge;
        }

        public Product()
        {
        }
    }
}
