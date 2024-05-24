using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using BusinessLogic.Specifications;
using DataAccess.Data;
using DataAccess.Data.Entities;
using DataAccess.Repositories;
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
        private readonly IFileService fileService;

        //private readonly ShopDbContext context;

        public ProductsService(IMapper mapper, 
                                IRepository<Product> productsR,
                                IRepository<Category> categoriesR,
                                IFileService fileService)
        {
            this.mapper = mapper;
            this.productsR = productsR;
            this.categoriesR = categoriesR;
            this.fileService = fileService;
        }

        public async Task CleanUpProductImages()
        {
            var imagePaths = productsR.GetAll().Select(x => x.ImageUrl).ToArray();
            await fileService.DeleteProductImageExcept(imagePaths);
        }

        public void Create(CreateProductModel product)
        {
            productsR.Insert(mapper.Map<Product>(product));
            productsR.Save();
        }

        public async Task Delete(int id)
        {
            if (id < 0) throw new HttpException(Errors.IdMustPositive, HttpStatusCode.BadRequest);

            // delete product by id
            var product = productsR.GetById(id);

            if (product == null) throw new HttpException(Errors.ProductNotFound, HttpStatusCode.NotFound);

            await fileService.DeleteProductImage(product.ImageUrl);

            productsR.Delete(product);
            productsR.Save();
        }

        public async Task Edit(EditProductModel product)
        {
            if (product.NewImage != null)
            {
                await fileService.DeleteProductImage(product.ImageUrl);
                product.ImageUrl = await fileService.SaveProductImage(product.NewImage);
            }

            productsR.Update(mapper.Map<Product>(product));
            productsR.Save();
        }

        public async Task<ProductDto?> Get(int id)
        {
            if (id < 0) throw new HttpException(Errors.IdMustPositive, HttpStatusCode.BadRequest);

            var item = await productsR.GetItemBySpec(new ProductSpecs.ById(id));
            if (item == null) throw new HttpException(Errors.ProductNotFound, HttpStatusCode.NotFound);

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

        public async Task<IEnumerable<ProductDto>> Get(IEnumerable<int> ids)
        {
            //return mapper.Map<List<ProductDto>>(context.Products
            //    .Include(x => x.Category)
            //    .Where(x => ids.Contains(x.Id))
            //    .ToList());
            return mapper.Map<List<ProductDto>>(await productsR.GetListBySpec(new ProductSpecs.ByIds(ids)));
        }

        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            return mapper.Map<List<ProductDto>>(await productsR.GetListBySpec(new ProductSpecs.All()));
        }

        public IEnumerable<CategoryDto> GetAllCategories()
        {
            return mapper.Map<List<CategoryDto>>(categoriesR.GetAll());
        }
    }
}
