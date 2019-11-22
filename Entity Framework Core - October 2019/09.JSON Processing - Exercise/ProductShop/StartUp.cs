using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ProductShop.Data;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            //Creating database
            using (var db = new ProductShopContext())
            {
                //db.Database.EnsureCreated();

                //default working directory in .Net bin/debug/netcoreapp
                //var inputJson = File.ReadAllText("./../../../Datasets/categories-products.json");

                var result = GetCategoriesByProductsCount(db);

                Console.WriteLine(result);
            }
        }

        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            //Get all users from users.json
            var users = JsonConvert.DeserializeObject<User[]>(inputJson);

            //Add them to database
            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Length}";
        }

        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            var products = JsonConvert.DeserializeObject<Product[]>(inputJson);

            context.Products.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Length}";
        }

        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            var categories = JsonConvert.DeserializeObject<Category[]>(inputJson);

            var validCategories = categories
                .Where(x => x.Name != null
                && x.Name.Length >= 3
                && x.Name.Length <= 13)
                .ToArray();

            context.Categories.AddRange(validCategories);
            int rows = context.SaveChanges();

            return $"Successfully imported {rows}";
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            var categoryProducts = JsonConvert.DeserializeObject<CategoryProduct[]>(inputJson);

            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Length}";
        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context
                .Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .OrderBy(p => p.Price)
                .Select(p => new
                {
                    name = p.Name,
                    price = p.Price,
                    seller = $"{p.Seller.FirstName} {p.Seller.LastName}"
                })
                .ToList();

            var json = JsonConvert.SerializeObject(products,Formatting.Indented);


            return json;
        }

        public static string GetSoldProducts(ProductShopContext context)
        {
            var soldProducts = context
                .Users
                .Where(u => u.ProductsSold.Count > 0 // get all users with at least 1 sold item with buyer.
                && u.ProductsSold.Any(ps => ps.Buyer != null))
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Include(u => u.ProductsSold)
                .ToList();

            var json = JsonConvert.SerializeObject(soldProducts, Formatting.Indented);

            return json;
        }

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context
                .Categories
                .OrderByDescending(c => c.CategoryProducts.Count)
                .Select(c => new
                {
                    Category = c.Name,
                    ProductsCount = c.CategoryProducts.Count,
                    AveragePrice = $"{c.CategoryProducts.Average(cp => cp.Product.Price):f2}",
                    TotalRevenue = $"{c.CategoryProducts.Sum(cp =>cp.Product.Price):f2}"
                });

            DefaultContractResolver contractResolver = new DefaultContractResolver()
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            var jsonResult = JsonConvert.SerializeObject(categories, new JsonSerializerSettings()
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });

            return jsonResult;
        }


        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var users = context
                .Users
                .Where(u => u.ProductsSold.Any(p => p.Buyer != null))
                
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    age = u.Age,
                    soldProducts = new
                    {
                        count = u.ProductsSold
                                 .Where(p => p.Buyer !=null)
                                 .Count(),
                        products = u.ProductsSold
                                .Where(p => p.Buyer != null)
                                .Select(ps => new
                                {
                                    name = ps.Name,
                                    price = ps.Price
                                })
                                .ToList()
                    }
                })
                .OrderByDescending(u => u.soldProducts.count)
                .ToList();

            var userOutput = new
            {
                usersCount = users.Count,
                users = users
            };

            var json = JsonConvert.SerializeObject(userOutput, new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });

            return json;
        }
    }
}