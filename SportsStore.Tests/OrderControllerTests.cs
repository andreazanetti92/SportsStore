﻿using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.DataProvider.Interfaces;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Tests
{
    public class OrderControllerTests
    {
        [Fact]
        public void Cannot_Checkout_Empty_Cart()
        {
            // Arrange
            // - create a mock repository
            Mock<IOrderRepository> mock = new();
            // - create an empty cart
            Cart cart = new();
            // - create an empty order
            Order order = new();
            // create an instance of the controller
            OrderController target = new(mock.Object, cart);

            // Act
            ViewResult? result = target.Checkout(order) as ViewResult;

            // Assert - check that the order hasn't been stored
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
            // Assert - check that the method is returning the default view
            Assert.True(string.IsNullOrEmpty(result?.ViewName));
            // Assert - check that I am passing an invalid model to the view
            Assert.False(result?.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void Cannot_Checkout_Invalid_ShippingDetails()
        {
            // Arrange
            // - create a mock order repository
            Mock<IOrderRepository> mock = new();
            // - create a cart with one item
            Cart cart = new();
            cart.AddItem(new Product(), 1);
            // - create an instance of the controller
            OrderController target = new(mock.Object, cart);
            // - add an error to the model
            target.ModelState.AddModelError("error", "error");

            // Act - try to checkout
            ViewResult? result = target.Checkout(new Order()) as ViewResult;

            // Assert
            // - check that the order hasn't been passed stored
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
            // - check that the method is returning the default view
            Assert.True(string.IsNullOrEmpty(result?.ViewName));
            // - check that I am passing an invalid model to view
            Assert.False(result?.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void Can_Checkout_And_Submit_Order()
        {
            // Arrange
            // - create a mock repository
            Mock<IOrderRepository> mock = new();
            // - create a cart with 1 item
            Cart cart = new();
            cart.AddItem(new Product(), 1);
            // - create an instance of the controller
            OrderController target = new OrderController(mock.Object, cart);

            // Act
            // - try to checkout 
            RedirectToPageResult? result = target.Checkout(new Order()) as RedirectToPageResult;

            // Assert
            // - check that the order has been stored
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Once);
            // - check the method is redirecting the completed action
            Assert.Equal("/Completed", result?.PageName);
        }
    }
}
