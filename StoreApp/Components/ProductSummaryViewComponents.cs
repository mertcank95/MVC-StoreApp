using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreApp.Components
{
    public class ProductSummaryViewComponents:ViewComponent
    {
        private readonly IServiceManager _manager;

        public ProductSummaryViewComponents(IServiceManager manager)
        {
            _manager = manager;
        }

        public string Invoke()
        {
            return _manager.ProductService.GetAllProducts(false).Count().ToString();
        }
    }
}
