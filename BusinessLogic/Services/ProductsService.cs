using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Data.Entities;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Shop_Api_PV221;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IMapper mapper;
        private readonly IRepository<Product> productsR;
        private readonly IRepository<Category> categoriesR;
        //private readonly ShopDbContext context;

        public ProductsService(IMapper mapper, 
                                IRepository<Product> productsR,
                                IRepository<Category> categoriesR)
        {
            this.mapper = mapper;
            this.productsR = productsR;
            this.categoriesR = categoriesR;
        }

        public void Create(CreateProductModel product)
        {
            productsR.Insert(mapper.Map<Product>(product));
            productsR.Save();
        }

        public void Delete(int id)
        {
            if (id < 0) throw new HttpException("Id must be positive:)", HttpStatusCode.BadRequest);

            // delete product by id
            var product = productsR.GetByID(id);

            if (product == null) throw new HttpException("Product not found.", HttpStatusCode.NotFound);

            productsR.Delete(product);
            productsR.Save();
        }

        public void Edit(ProductDto product)
        {
            productsR.Update(mapper.Map<Product>(product));
            productsR.Save();
        }

        public ProductDto? Get(int id)
        {
            if (id < 0) throw new HttpException("Id must be positive:)", HttpStatusCode.BadRequest);

            var item = productsR.GetByID(id);
            if (item == null) throw new HttpException("Product not found.", HttpStatusCode.NotFound);

            // load related entity
            //context.Entry(item).Reference(x => x.Category).Load();

            // convert entity type to DTO
            // 1 - using manually (handmade)
            //var dto = new ProductDto()
            //{
            //    Id = product.Id,
            //    CategoryId = product.CategoryId,
            //    Description = product.Description,
            //    Discount = product.Discount,
            //    ImageUrl = product.ImageUrl,
            //    InStock = product.InStock,
            //    Name = product.Name,
            //    Price = product.Price,
            //    CategoryName = product.Category.Name
            //};
            // 2 - using AutoMapper
            var dto = mapper.Map<ProductDto>(item);

            return dto;
        }

        public IEnumerable<ProductDto> Get(IEnumerable<int> ids)
        {
            //return mapper.Map<List<ProductDto>>(context.Products
            //    .Include(x => x.Category)
            //    .Where(x => ids.Contains(x.Id))
            //    .ToList());
            return mapper.Map<List<ProductDto>>(productsR.Get(x => ids.Contains(x.Id), includeProperties: "Category"));
        }

        public IEnumerable<ProductDto> GetAll()
        {
            return mapper.Map<List<ProductDto>>(productsR.GetAll());
        }

        public IEnumerable<CategoryDto> GetAllCategories()
        {
            return mapper.Map<List<CategoryDto>>(categoriesR.GetAll());
        }
    }
}
