﻿using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop_Api_PV221.Helpers;

namespace Shop_Api_PV221.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        [HttpGet("all")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policies.ADULT)]
        public async Task<IActionResult> Get()
        {
            return Ok(await productsService.GetAll());
        }

        //[Authorize] // based on cookies
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] // based on JWT
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            return Ok(await productsService.Get(id));
        }

        [HttpPost]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policies.PREMIUM_CLIENT)]
        public IActionResult Create([FromForm] CreateProductModel model)
        {
            productsService.Create(model);
            return Ok();
        }


        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Edit([FromForm] EditProductModel model)
        {
            await productsService.Edit(model);
            return Ok();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.ADMIN)]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            await productsService.Delete(id);
            return Ok();
        }

        [HttpGet("categories")]
        public IActionResult GetCategories()
        {
            return Ok(productsService.GetAllCategories());
        }

        [HttpDelete("cleanup")]
        public async Task<IActionResult> CleanUp()
        {
            await productsService.CleanUpProductImages();
            return Ok();
        }
    }
}
