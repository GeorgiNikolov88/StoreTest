using Store;
using Store.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Store
{
    class CRUDProduct
    {
        Product product = new Product();
        //bool correct;
        //Метод за създаване на нов обект
        //static List<string> languageInterface = Startup.languageInterface;
        public void CreateNewProduct(List<string> langageInterface)
        {
            Console.Write(langageInterface[12]);
            string brand = CheckIfItemExists(Console.ReadLine());
            if (brand == string.Empty)
            {
                return;
            }
            Console.Write(langageInterface[13]);
            decimal price =InputChecker.CheckIfDecimal();
            Console.Write(langageInterface[14]);
            int inStock = InputChecker.CheckIfInt();
            Console.WriteLine(langageInterface[15]);
            int type = InputChecker.CheckTypeInput(0);
            Console.Write(langageInterface[16]);            
            int maxStock = InputChecker.CheckIfInt();
            Console.Write(langageInterface[17]);
            decimal overcharge = InputChecker.CheckIfDecimal();
            product = new Product(brand, price, inStock, type, maxStock, overcharge);

            using (var context = new StoreContext())
            {
                context.Products.Add(product);
                context.SaveChanges();
            }            
        }

        //Метод за редактиране на даден обект от списъка с продукти в магазина
        public void EditProduct()
        {
            int itemToEdit = SellAndRestock.FindItem("edit");
            while (itemToEdit == -1)
            {
                Console.WriteLine(Startup.languageInterface[36]);
                itemToEdit = SellAndRestock.FindItem("Edit");                
            }
            Console.WriteLine(Startup.languageInterface[26], itemToEdit);
            ConsoleKey check = InputChecker.CheckIfEnter();
            if (check == ConsoleKey.Enter)
            {
                using (var context = new StoreContext())
                {
                    var selectedProduct = context.Products.Single(id => id.ProductID == itemToEdit);
                    //if (selectedProduct.Brand == itemToEdit)
                    //{
                        Console.WriteLine(Startup.languageInterface[12]);
                        product.Brand = EditItemProperty(selectedProduct.Brand);
                        Console.WriteLine(Startup.languageInterface[29]);
                        selectedProduct.Price = EditItemProperty(selectedProduct.Price);
                        Console.WriteLine(Startup.languageInterface[30]);
                        selectedProduct.InStock = EditItemProperty(selectedProduct.InStock);
                        Console.WriteLine(Startup.languageInterface[31]);
                        selectedProduct.Type = InputChecker.CheckTypeInput(selectedProduct.Type);
                        Console.WriteLine(Startup.languageInterface[32]);
                        selectedProduct.MaxStock = EditItemProperty(selectedProduct.MaxStock);
                        Console.WriteLine(Startup.languageInterface[33]);
                        selectedProduct.Overcharge = EditItemProperty(selectedProduct.Overcharge);
                    //}
                    context.SaveChanges();
                }
            }
            else
            {
                Console.WriteLine(Startup.languageInterface[34]);
            }            
        }

        //Метод за изтриване на обект от списъка с продукти
        public void DeleteItem()
        {
            Console.WriteLine(Startup.languageInterface[22]);
            ConsoleKey check = InputChecker.CheckIfEnter();
            if (check == ConsoleKey.Enter)
            {                
                int index = SellAndRestock.FindItem("edit");
                //Console.WriteLine(Startup.languageInterface[23], list[index].Brand);
                check = InputChecker.CheckIfEnter();
                if (check == ConsoleKey.Enter)
                {
                    using (var context = new StoreContext())
                    {
                        var itemToDelete = context.Products.Single(id=>id.ProductID == index);
                        context.Products.Remove(itemToDelete);
                    }
                }
                else
                {
                    //Console.WriteLine(Startup.languageInterface[25], list[index].Brand);
                    //return list;
                }

            }
            else
            {
                Console.WriteLine(Startup.languageInterface[25]);
            }
            //return list;
        }        

        //Проверка за съществуването да даден хранителен тип от foodType списъка в Product.cs
        

        //Проверка дали обекта съществува
        private string CheckIfItemExists(string brand)
        {
            using (var context = new StoreContext())
            {
                var productList = context.Products.ToList();
                foreach (var item in productList)
                {
                    while (brand == item.Brand)
                    {
                        Console.WriteLine(Startup.languageInterface[37]);
                        Console.WriteLine(Startup.languageInterface[0]);
                        ConsoleKey answer = InputChecker.CheckIfEnter();
                        if (answer == ConsoleKey.Enter)
                        {
                            brand = Console.ReadLine();
                        }
                        else
                        {
                            return string.Empty;
                        }
                    }
                }
            }
            return brand;
        }

        public static string CheckIfTypeExists(string foodType)
        {
            using (var context = new StoreContext())
            {
                var types = context.ProductTypes.ToList();
                foreach (var item in types)
                {
                    while (foodType.ToLower() == item.PropertyName.ToLower())
                    {
                        Console.WriteLine(Startup.languageInterface[72]);
                        Console.WriteLine(Startup.languageInterface[0]);
                        ConsoleKey answer = InputChecker.CheckIfEnter();
                        if (answer == ConsoleKey.Enter)
                        {
                            foodType = Console.ReadLine();
                        }
                        else
                        {
                            return string.Empty;
                        }
                    }
                }
            }
            return foodType;
        }

        //Edit item properties overloads
        private string EditItemProperty(string property)
        {
            string tempProperty = property;
            Console.Write(Startup.languageInterface[27]);
            property = Console.ReadLine();
            if (property == "")
            {
                Console.WriteLine(Startup.languageInterface[34]);                
            }

            return tempProperty;
        }

        private int EditItemProperty(int property)
        {
            Console.WriteLine(Startup.languageInterface[77], property);
            bool correct = int.TryParse(Console.ReadLine(), out int input);
            if (string.IsNullOrEmpty(input.ToString()) || input == property)
            {
                Console.WriteLine(Startup.languageInterface[34]);
                return property;
            }
            else
            {
                while (correct == false)
                {
                    if (input.ToString() == "" || input == property)
                    {
                        Console.WriteLine(Startup.languageInterface[34]);
                        return property;
                    }
                    Console.Write(Startup.languageInterface[1]);
                    correct = int.TryParse(Console.ReadLine(), out input);
                }
            }
            return input;
        }

        private decimal EditItemProperty(decimal property)
        {
            Console.WriteLine(Startup.languageInterface[77],property);
            bool correct = decimal.TryParse(Console.ReadLine(), out decimal input);
            if (string.IsNullOrEmpty(input.ToString()) || input == property)
            {
                Console.WriteLine(Startup.languageInterface[34]);
                return property;
            }
            else
            {
                while (correct == false)
                {
                    if (input.ToString() == "" || input == property)
                    {
                        Console.WriteLine(Startup.languageInterface[34]);
                        return property;
                    }
                    Console.Write(Startup.languageInterface[1]);
                    correct = decimal.TryParse(Console.ReadLine(), out input);
                }
            }
            return input;
        }
    }
}
