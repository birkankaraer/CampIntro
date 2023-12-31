﻿using System.Threading.Channels;

namespace LinqProject
{
    internal class Program : Test
    {
        static void Main(string[] args)
        {
            List<Category> categories = new List<Category>
            {
                new Category{CategoryId=1, CategoryName="Bilgisayar"},
                new Category{CategoryId=2, CategoryName="Telefon"}
            };
            List<Product> products = new List<Product>
            {
                new Product{ProductId=1,CategoryId=1,ProductName="Acer Laptop",QuantityPerUnit="32 GB Ram", UnitPrice=10000, UnitsInStock=6},
                new Product{ProductId=2,CategoryId=1,ProductName="Asus Laptop",QuantityPerUnit="16 GB Ram", UnitPrice=8000, UnitsInStock=3},
                new Product{ProductId=3,CategoryId=1,ProductName="Hp Laptop",QuantityPerUnit="8 GB Ram", UnitPrice=6000, UnitsInStock=2 },
                new Product{ProductId=4,CategoryId=2,ProductName="Samsung Telefon",QuantityPerUnit="4 GB Ram", UnitPrice=5000, UnitsInStock=15},
                new Product{ProductId=5,CategoryId=2,ProductName="Apple Telefon",QuantityPerUnit="4 GB Ram", UnitPrice=18000, UnitsInStock=0}
            };
            //test(products);

            //AnyTest(products);

            //FindTest(products);
            // FindAllTest(products);
            //AscDescTest(products);
            //ClassicLinqTest(products);
            var result = from p in products
                         join c in categories
                         on p.CategoryId equals c.CategoryId
                         where p.UnitPrice >= 8000
                         orderby p.UnitPrice descending
                         select new ProductDto {CategoryName=c.CategoryName, ProductId = p.ProductId, ProductName = p.ProductName, CategoryId= c.CategoryId };
            foreach (var product in result)
            {
                Console.WriteLine(product.ProductName +"  "+ product.CategoryName);
            }
        }

        private static void ClassicLinqTest(List<Product> products)
        {
            var result = from p in products
                         where p.UnitPrice > 7000
                         orderby p.UnitPrice descending, p.ProductName ascending
                         select p;
            foreach (var product in result)
            {
                Console.WriteLine(product.ProductName);
            }
        }

        private static void AscDescTest(List<Product> products)
        {
            //Single Line Query
            var result = products.Where(p => p.ProductName.Contains("top")).OrderBy(p => p.UnitPrice).ThenByDescending(p => p.ProductName);
            foreach (var p in result)
            {
                Console.WriteLine(p.ProductName);
            }
        }

        private static void FindAllTest(List<Product> products)
        {
            var result = products.FindAll(p => p.ProductName.Contains("top"));
            Console.WriteLine(result);
        }

        private static void FindTest(List<Product> products)
        {
            var result = products.Find(p => p.ProductId == 6);
            Console.WriteLine(result.ProductName + ":" + result.QuantityPerUnit);
        }

        private static void AnyTest(List<Product> products)
        {
            var result = products.Any(p => p.ProductName == "Dell Laptop");
            Console.WriteLine(result);
        }

        private static void test(List<Product> products)
        {
            foreach (var product in products)
            {
                if (product.UnitPrice > 5000 && product.UnitsInStock > 5)
                {
                    Console.WriteLine(product.ProductName);
                }
            }
            Console.WriteLine("------------ALGORİTMİK-----------");

            var result = products.Where(p => p.UnitPrice > 5000 && p.UnitsInStock > 5);
            foreach (var p in result)
            {
                Console.WriteLine(p.ProductName);
            }
            Console.WriteLine("----------LINQ---------");
        }
    }
    class Product
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
    }
    class Category
    {
        public int CategoryId { get; set;}
        public string CategoryName { get; set; }
    }
    class ProductDto
    {
        public int ProductId { get; set;}
        public int CategoryId { get; set;}
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
    }
}