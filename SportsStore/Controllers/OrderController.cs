using Microsoft.AspNetCore.Mvc;
using SportsStore.DataProvider.Interfaces;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository _repo;
        private Cart cart;

        public OrderController(IOrderRepository orderRepository, Cart cartService)
        {
            _repo = orderRepository;
            cart = cartService;
        }

        public ViewResult Checkout() => View(new Order());

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if(cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty");
            }
            if(ModelState.IsValid)
            {
                order.Lines = cart.Lines.ToArray();
                _repo.SaveOrder(order);
                cart.Clear();
                return RedirectToPage("/Completed", new { Id = order.Id });
            }
            else
            {
                return View();
            }
        }
    }
}
