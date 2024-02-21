using DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public static class ModelBuilderExtensions
    {
        public static void SeedData(this ModelBuilder builder)
        {
            builder.Entity<Category>().HasData(new[]
            {
                new Category() { Id = (int)Categories.Electronics, Name = "Electronics" },
                new Category() { Id = (int)Categories.Sport, Name = "Sport" },
                new Category() { Id = (int)Categories.Fashion, Name = "Fashion" },
                new Category() { Id = (int)Categories.HomeAndGarden, Name = "Home & Garden" },
                new Category() { Id = (int)Categories.Transport, Name = "Transport" },
                new Category() { Id = (int)Categories.ToysAndHobbies, Name = "Toys & Hobbies" },
                new Category() { Id = (int)Categories.MusicalInstruments, Name = "Musical Instruments" },
                new Category() { Id = (int)Categories.Art, Name = "Art" },
                new Category() { Id = (int)Categories.Other, Name = "Other" }
            });


            builder.Entity<Product>().HasData(new[]
            {
                new Product() { Id = 1, Name = "iPhone X", CategoryId = (int)Categories.Electronics, Discount = 10, Price = 650, ImageUrl = "https://applecity.com.ua/image/cache/catalog/0iphone/ipohnex/iphone-x-black-1000x1000.png" },
                new Product() { Id = 2, Name = "PowerBall", CategoryId = (int)Categories.Sport, Price = 45.5M, ImageUrl = "https://http2.mlstatic.com/D_NQ_NP_727192-CBT53879999753_022023-V.jpg" },
                new Product() { Id = 3, Name = "Nike T-Shirt", CategoryId = (int)Categories.Fashion, Discount = 15, Price = 189, InStock = true, ImageUrl = "https://www.seekpng.com/png/detail/316-3168852_nike-air-logo-t-shirt-nike-t-shirt.png" },
                new Product() { Id = 4, Name = "Samsung S23", CategoryId = (int)Categories.Electronics, Discount = 5, Price = 1200, InStock = true, ImageUrl = "https://sota.kh.ua/image/cache/data/Samsung-2/samsung-s23-s23plus-blk-01-700x700.webp" },
                new Product() { Id = 5, Name = "Air Ball", CategoryId = (int)Categories.ToysAndHobbies, Price = 50, ImageUrl = "https://cdn.shopify.com/s/files/1/0046/1163/7320/products/69ee701e-e806-4c4d-b804-d53dc1f0e11a_grande.jpg" },
                new Product() { Id = 6, Name = "MacBook Pro 2019", CategoryId = (int)Categories.Electronics, Discount = 10, InStock = true, Price = 1200, ImageUrl = "https://newtime.ua/image/import/catalog/mac/macbook_pro/MacBook-Pro-16-2019/MacBook-Pro-16-Space-Gray-2019/MacBook-Pro-16-Space-Gray-00.webp" }
            });
        }
    }
}
