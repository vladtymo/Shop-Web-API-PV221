﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Data
{
    //public class SampleContextFactory : IDesignTimeDbContextFactory<ShopDbContext>
    //{
    //    public ShopDbContext CreateDbContext(string[] args)
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<ShopDbContext>();

    //        ConfigurationBuilder builder = new ConfigurationBuilder();
    //        builder.SetBasePath(Directory.GetCurrentDirectory());
    //        builder.AddJsonFile("appsettings.json");
    //        IConfigurationRoot config = builder.Build();

    //        string? connectionString = config.GetConnectionString("AzureDb");
    //        optionsBuilder.UseSqlServer(connectionString);
    //        return new ShopDbContext(optionsBuilder.Options);
    //    }
    //}
}
