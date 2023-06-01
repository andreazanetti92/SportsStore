using Microsoft.AspNetCore.Mvc;
using SportsStore.DataProvider.Interfaces;

namespace SportsStore.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        public IStoreRepository _repo;
        
        public NavigationMenuViewComponent(IStoreRepository repo)
        {
            _repo = repo;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["Category"];
            return View(_repo.Products
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x));
        }
    }
}
