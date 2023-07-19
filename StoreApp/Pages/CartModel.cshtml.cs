using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Contracts;
using StoreApp.Infrastructe.Extension;

namespace StoreApp.Pages
{
    public class CartModel : PageModel
    {
        private readonly IServiceManager _manager;
        public Cart Cart { get; set; } //IoC
        public CartModel(IServiceManager manager,Cart cartService)
        {
            _manager = manager;
            Cart = cartService;
         
        }

        public string ReturnUrl { get; set; } = "/";
        public void OnGet(string returnUrl)
        {
            ReturnUrl= returnUrl ?? "/";
            //Cart = HttpContext.Session.GetJson<Cart>("cart")?? new Cart();//varsa cartý ver yoksa üret
        }

        public IActionResult OnPost(int productId,string returnUrl)
        {
            Product? product = _manager.ProductService
                .GetOneProduct(productId, false);

            if (product is not null) 
            {
                Cart.AddItem(product, 1);
                
            }

            return RedirectToPage(new {returnUrl= returnUrl } );//return url
        }

        public IActionResult OnPostRemove(int id,string stringUrl) 
        {
            Cart.RemoveLine(Cart.Lines
                .First(c1 => c1.Product.ProductId.Equals(id)).Product);
            return Page();
        }

    }
}
