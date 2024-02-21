using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Data.Entities;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    internal class OrdersService : IOrdersService
    {
        private readonly IMapper mapper;
        private readonly IRepository<Order> orderR;
        private readonly IRepository<Product> productR;
        private readonly ICartService cartService;
        private readonly IEmailSender emailSender;
        //private readonly IViewRender viewRender;

        public OrdersService(IMapper mapper, 
                            IRepository<Order> orderR,
                            IRepository<Product> productR,
                            ICartService cartService,
                            //IViewRender viewRender,
                            IEmailSender emailSender)
        {
            this.mapper = mapper;
            this.orderR = orderR;
            this.productR = productR;
            this.cartService = cartService;
            this.emailSender = emailSender;
            //this.viewRender = viewRender;
        }

        public async Task Create(string userId)
        {
            var ids = cartService.GetProductIds();
            var products = productR.Get(x => ids.Contains(x.Id)).ToList();

            var order = new Order()
            {
                Date = DateTime.Now,
                UserId = userId,
                Products = products,
                TotalPrice = products.Sum(x => x.Price),
            };

            orderR.Insert(order);
            orderR.Save();

            //context.Entry(order).Reference(x => x.User).Load();

            // send order summary email
            //var orderSummary = mapper.Map<OrderSummaryModel>(order);
            //string html = viewRender.Render("MailTemplates/OrderSummary", orderSummary);

            //await emailSender.SendEmailAsync("tymo.vlad@gmail.com", $"Order #{orderSummary.Number}", html);
        }

        public IEnumerable<OrderDto> GetAllByUser(string userId)
        {
            var items = orderR.Get(x => x.UserId == userId, includeProperties: "Products");
            return mapper.Map<IEnumerable<OrderDto>>(items);
        }
    }
}
