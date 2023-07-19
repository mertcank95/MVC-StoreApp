using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace StoreApp.Components
{
    public class CartSummaryViewComponents : ViewComponent
    {
        private readonly Cart _cart;

        public CartSummaryViewComponents(Cart cart)
        {
            _cart = cart;
        }

        public string Invoke()
        {
            return _cart.Lines.Count().ToString();
        }

    }






}
