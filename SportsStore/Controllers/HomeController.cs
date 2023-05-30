using Microsoft.AspNetCore.Mvc;
using SportsStore.DataProvider.Interfaces;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private IStoreRepository _repo;
        public int pageSize = 3;


        public HomeController(IStoreRepository repo)
        {
            _repo = repo;
        }

        //public IActionResult Index() => View(_repo.Products);

        public ViewResult Index(int productPage = 1)
            => View(new ProductsListViewModel { 
                Products = _repo.Products
                .OrderBy(p => p.ProductID)
                .Skip((productPage - 1) * pageSize)
                .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = pageSize,
                    TotalItems = _repo.Products.Count()
                }
            });
    }
}
