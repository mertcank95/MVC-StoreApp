using Microsoft.AspNetCore.Mvc;

namespace StoreApp.Components
{
    public class ProductFilterMenuViewComponents : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
