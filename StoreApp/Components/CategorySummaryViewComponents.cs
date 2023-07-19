using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreApp.Components
{
    public class CategorySummaryViewComponents : ViewComponent
    {
        private readonly IServiceManager _manager;

        public CategorySummaryViewComponents(IServiceManager manager)
        {
            _manager = manager;
        }

        public string Invoke()
        {
            return _manager
                .CategoryService
                .GetAllCategories(false)
                .Count()
                .ToString();
        }
    }
}
