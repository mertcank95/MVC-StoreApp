using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreApp.Components
{
    public class UserSummaryViewComponents : ViewComponent
    {
        private readonly IServiceManager _manager;

        public UserSummaryViewComponents(IServiceManager manager)
        {
            _manager = manager;
        }

        public string Invoke()
        {
            return _manager
                .AutService
                .GetAllUsers()
                .Count()
                .ToString();
        }
    }
}
