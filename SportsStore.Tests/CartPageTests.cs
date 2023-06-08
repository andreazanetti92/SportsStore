using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using SportsStore.DataProvider.Interfaces;
using SportsStore.Models;
using SportsStore.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SportsStore.Tests
{
    public class CartPageTests
    {
        [Fact]
        public void Can_Load_Cart()
        {
            // Arrange - create a mock repository
            Product p1 = new() { ProductID = 1, Name = "P1" };
            Product p2 = new() { ProductID = 2, Name = "P2" };
            Mock<IStoreRepository> mockedRepo = new();
            mockedRepo.Setup(m => m.Products).Returns((new Product[]
            {
                p1, p2
            }
            ).AsQueryable<Product>());

            // Arrange - Create a cart
            Cart testCart = new Cart();
            testCart.AddItem(p1, 2);
            testCart.AddItem(p2, 1);

            // Arrange - Create a mock page and session 
            Mock<ISession> mockedSession = new();
            byte[] data = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(testCart));
            mockedSession.Setup(c => c.TryGetValue(It.IsAny<string>(), out data));

            Mock<HttpContext> mockedContext = new();
            mockedContext.SetupGet(c => c.Session).Returns(mockedSession.Object);

            // Action
            CartModel cartModel = new(mockedRepo.Object, testCart);
            //{
            //    PageContext = new PageContext(new ActionContext
            //    {
            //        HttpContext = mockedContext.Object,
            //        RouteData = new Microsoft.AspNetCore.Routing.RouteData(),
            //        ActionDescriptor = new PageActionDescriptor()
            //    })
            //};
            cartModel.OnGet("myUrl");

            // Assert
            Assert.Equal(2, cartModel.Cart?.Lines.Count);
            Assert.Equal("myUrl", cartModel.ReturnUrl);
        }

        [Fact]
        public void Can_Update_Cart()
        {
            // Arrange - Create a mock repository
            Mock<IStoreRepository> mockedRepo = new();
            mockedRepo.Setup(m => m.Products).Returns((new Product[]
            {
                new Product { ProductID = 1, Name = "P1"}
            }
            ).AsQueryable<Product>());

            Cart? testCart = new();

            Mock<ISession> mockedSession = new();

            mockedSession.Setup(s => s.Set(It.IsAny<string>(), It.IsAny<byte[]>()))
                .Callback<string, byte[]>((key, val) =>
                {
                    testCart = JsonSerializer.Deserialize<Cart>(Encoding.UTF8.GetString(val));
                });

            Mock<HttpContext> mockedHttpContext = new();
            mockedHttpContext.SetupGet(c => c.Session).Returns(mockedSession.Object);

            // Action
            CartModel cartModel = new(mockedRepo.Object, testCart);
            //{
            //    PageContext = new PageContext(new ActionContext
            //    {
            //        HttpContext = mockedHttpContext.Object,
            //        RouteData = new Microsoft.AspNetCore.Routing.RouteData,
            //        ActionDescriptor = new PageActionDescriptor()
            //    })
            //};

            // Assert
            Assert.Single(testCart.Lines);
            Assert.Equal("P1", testCart.Lines.First().Product.Name);
            Assert.Equal(1, testCart.Lines.First().Quantity);
        }
    }
}
