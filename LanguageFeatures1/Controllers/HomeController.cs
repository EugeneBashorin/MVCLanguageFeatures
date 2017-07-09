using LanguageFeatures1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LanguageFeatures1.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "Navigate to a URL to show an example";
        }

        public ViewResult AutoProperty()
        {
            Product myProduct = new Product();
            myProduct.Name = "Heater";
            string productName = myProduct.Name;
            return View("Result", (Object)String.Format("Prodact name: {0}", productName));
        }

        public ViewResult CreateProduct()
        {
            //Product myProduct = new Product();
            //myProduct.ProductID = 100;
            //myProduct.Name = "Heater";
            //myProduct.Description = "Inrfared Heaters with convections";
            //myProduct.Price = 2430M;
            //myProduct.Category = "Infrared Heaters";

            Product myProduct = new Product
            {
                ProductID = 100,
                Name = "Heater",
                Description = "Inrfared Heaters with convections",
                Price = 2430M,
                Category = "Infrared Heaters"
            };

            return View("Result", (object)String.Format("Category: {0}", myProduct.Category));
        }
        public ViewResult CreateCollection()
        {
            string[] stringArray = { "apple", "orange", "plum" };
            List<int> intList = new List<int> { 10, 20, 30, 40 };
            Dictionary<string, int> myDict = new Dictionary<string, int>{
                {"apple",10 }, { "orange", 20 }, { "plum", 30 }
            };
            return View("Result", (Object)stringArray[1]);
        }

        public ViewResult UseExtension()
        {
            ShoppingCart cart = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product { Name = "Heater", Price = 2400M },
                    new Product { Name = "WaterCleaner", Price = 4030M },
                    new Product { Name = "TermoRegulator", Price = 820M },
                    new Product { Name = "Airconditioner", Price = 6212M }
                }
            };
            //Get common cost in basket
            decimal cartTotal = cart.TotalPrices();
            return View("Result", (Object)String.Format("Total: {0:c}", cartTotal));
        }

        public ViewResult UseExtensionEnumerable()
        {
            IEnumerable<Product> products = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product { Name = "Heater", Price = 2400M },
                    new Product { Name = "WaterCleaner", Price = 4030M },
                    new Product { Name = "TermoRegulator", Price = 820M },
                    new Product { Name = "Airconditioner", Price = 6212M }
                }
            };
            //Create and fill array of object Product
            Product[] productArray = {
                    new Product { Name = "Heater", Price = 2400M },
                    new Product { Name = "WaterCleaner", Price = 4030M },
                    new Product { Name = "TermoRegulator", Price = 820M },
                    new Product { Name = "Airconditioner", Price = 6212M }
            };
            //Get common cost in basket
            decimal cartTotal = products.TotalPrices();
            decimal arrayTotal = productArray.TotalPrices();
            return View("Result", (Object)String.Format("Cart Total: {0}, Array Totel: {1}", cartTotal, arrayTotal));
        }

        public ViewResult UseFilterExtensionMethod()
        {
            IEnumerable<Product> products = new ShoppingCart
            {
                Products = new List<Product> {
                    new Product { Name = "Heater", Price = 2400M, Category = "ClimateDevices" },
                    new Product { Name = "WaterCleaner", Price = 4030M, Category = "WaterDevices" },
                    new Product { Name = "TermoRegulator", Price = 820M, Category= "MessureDevices" },
                    new Product { Name = "Airconditioner", Price = 6212M,Category = "ClimateDevices" }
                }

            };
            //D By lAmbda exspression without delegate generic Func
            //Also we can     prod => EvaluateProduct(prod) or if need couple or more param
            //we can:   (prod, count) => prod.Price > 20 && count > 0
            //And with some extralogic in Lambda exspression we use {...}
            /*(prod, count) => {
            ...несколько выражений кода
                return result;
            }*/
            decimal total = 0;
            foreach (Product prod in products.Filter(prod => prod.Category == "ClimateDevices" || /*add filter*/ prod.Price > 1000))
            {
                total += prod.Price;

            }
            //C By lAmbda exspression
            //Func<Product, bool> categoryFilter = prod => prod.Category == "ClimateDevices";
            //B Delegate with anonimus metod;
            //Func<Product, bool> categoryFilter = delegate (Product prod)
            // {
            //     return prod.Category == "ClimateDevices";
            // };

            //  decimal total = 0;

            //  foreach (Product prod in products.Filter(categoryFilter))
            //   {
            //      total += prod.Price;
            //   }
            //A To Ienumarable filters
            //foreach (Product prod in products.FilterByCategory("ClimateDevices"))
            //{
            //    total += prod.Price;
            //}
            return View("Result", (object)String.Format("Total: {0}", total));
        }
        public ViewResult CreateAnonArray()
        {
            var oddsAndEnds = new[]
            {
                new {Name ="MVC", Category = "Pattern"},
                new { Name = "Hat", Category = "Clothing"},
                new { Name = "Apple", Category = "Fruit"}
            };
            StringBuilder result = new StringBuilder();
            foreach (var item in oddsAndEnds)
            {
                result.Append(item.Name).Append(" ");
            }
            return View("Result", (object)result.ToString());
        }

        public ViewResult FindProducts()
        {
            Product[] products = {
                                    new Product {Name = "Kayak", Category = "Watersports", Price = 275M},
                                    new Product {Name = "Lifejacket", Category = "Watersports", Price = 48.95M},
                                    new Product {Name = "Soccer ball", Category = "Soccer", Price = 19.50M},
                                    new Product {Name = "Corner flag", Category = "Soccer", Price = 34.95M}
                                    };
            // определяем массив для результатов
            Product[] foundProducts = new Product[3];
            // сортируем содержание массива
            Array.Sort(products, (item1, item2) =>
            {
                return Comparer<decimal>.Default.Compare(item1.Price, item2.Price);
            });
            // получаем три первых элемента массива в качестве результата
            Array.Copy(products, foundProducts, 3);
            // создаем результат
            StringBuilder result = new StringBuilder();
            foreach (Product p in foundProducts)
            {
                result.AppendFormat("Price: {0} ", p.Price);
            }
            return View("Result", (object)result.ToString());
        }

        public ViewResult FindProductsLINQ()
        {
            Product[] products = {
                                    new Product {Name = "Kayak", Category = "Watersports", Price = 275M},
                                    new Product {Name = "Lifejacket", Category = "Watersports", Price = 48.95M},
                                    new Product {Name = "Soccer ball", Category = "Soccer", Price = 19.50M},
                                    new Product {Name = "Corner flag", Category = "Soccer", Price = 34.95M}
                                    };
            var foundProducts = from match/*совпадение*/ in products
                                orderby match.Price descending/*нисходящий*/
                                select new
                                {
                                    match.Name,
                                    match.Price
                                };
            //создаем результат
            int count = 0;
            StringBuilder result = new StringBuilder();
            foreach (var p in foundProducts)
            {
                result.AppendFormat("Price: {0}", p.Price);
                if (++count == 3)
                {
                    break;
                }
            }
            return View("Result", (object)result.ToString());
        }

        //Использование точечной нотации LINQ
        public ViewResult FindProductsLINQPointNotation()//+отложенные методы LINQ
        {
            Product[] products = {
                        new Product {Name = "Kayak", Category = "Watersports", Price = 275M},
                        new Product {Name = "Lifejacket", Category = "Watersports", Price = 48.95M},
                        new Product {Name = "Soccer ball", Category = "Soccer", Price = 19.50M},
                        new Product {Name = "Corner flag", Category = "Soccer", Price = 34.95M}
                        };
            var foundProducts = products.OrderByDescending(e => e.Price).Take(3).Select(e => new { e.Name, e.Price });
            products[2] = new Product { Name = "Stadium", Price = 79600M };// БУДЕТ участвовать в выборке т.к.отложенные методы LINQ

            /*Метод OrderByDescending сортирует элементы в исходных данных. В этом случае лямбдавыражение возвращает значение, которое мы хотим 
             использовать для сравнения. Метод Take возвращает указанное число элементов с начала цепочки результатов (это то, что мы не могли
             сделать, используя синтаксис запроса). Метод Select позволяет нам проектировать нужные результаты. В данном случае мы проектируем 
             анонимный объект, который содержит свойства Name и Price. Обратите внимание, что нам даже не нужно указывать имена свойств в анонимном
            типе. C# сделал вывод, основываясь на свойствах, которые мы вставили в метод Select */
            StringBuilder result = new StringBuilder();
            foreach (var p in foundProducts)
            {
                result.AppendFormat("Price: {0}", p.Price);
            }
            return View("Result", (object)result.ToString());
        }


        public ViewResult SumProducts() //+неотложенные методы LINQ выполняются немедленно
        {
            Product[] products = {
                new Product {Name = "Kayak", Category = "Watersports", Price = 275M},
                new Product {Name = "Lifejacket", Category = "Watersports", Price = 48.95M},
                new Product {Name = "Soccer ball", Category = "Soccer", Price = 19.50M},
                new Product {Name = "Corner flag", Category = "Soccer", Price = 34.95M}
                };
            var results = products.Sum(e => e.Price);
            products[2] = new Product { Name = "Stadium", Price = 79500M };// Не будет участвовать в выборке т.к.неотложенные методы LINQ
            return View("Result", (object)String.Format("Sum: {0:c}", results));
        }
    }
}