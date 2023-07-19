using Entities.Dtos;
using Entities.Models;
using Entities.RequestParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services;
using Services.Contracts;
using StoreApp.Models;
using System.Data;

namespace StoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IServiceManager _manager;

        public ProductController(IServiceManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index([FromQuery] ProductRequestParameters p)
        {
            ViewData["Title"] = "Products";
            var products = _manager.ProductService.GetAllProductsWithDetails(p);
            var pagination = new Pagination()
            {
                CurrentPage = p.PageNumber,
                ItemsPerPage = p.PageSize,
                TotalItems = _manager.ProductService.GetAllProducts(false).Count()
            };

            return View(new ProductListViewModel()//service çekilerbilir.
            {
                Products = products,
                Pagination = pagination
            });
        }

        public IActionResult Create()
        {
            ViewBag.Categories = GetCategoriesSelectList();

            //seçilebilir liste -> /seçilme şartı / gözükecek isim/default seçim değeri
            return View();
        }

        private SelectList GetCategoriesSelectList()
        {
            return new SelectList(_manager.CategoryService.GetAllCategories(false),
          "CategoryId",
          "CategoryName", "1");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ProductDtoForInsertion productDto,IFormFile file)
        {
            if (ModelState.IsValid)
            {
                //file operation
                string path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","images",file.FileName);

                using (var stream = new FileStream(path,FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                productDto.ImageUrl=String.Concat("/images/",file.FileName);
                _manager.ProductService.CreateProduct(productDto);
                TempData["success"] = $"{productDto.ProductName} Product Created";
                return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult Update([FromRoute(Name = "id")] int id)
        {
            ViewBag.Categories = GetCategoriesSelectList();
            var model = _manager.ProductService.GetOneProductForUpdate(id, false);
            ViewData["Title"] = model?.ProductName;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromForm] ProductDtoForUpdate productDto,IFormFile file)
        {
            if (ModelState.IsValid)
            { //file operation
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", 
                    "images", file.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                productDto.ImageUrl = String.Concat("/images/", file.FileName);
                _manager.ProductService.UpdateOneProduct(productDto);
                return RedirectToAction("Index");
            }
            return View();

        }

        [HttpGet]
        public IActionResult Delete([FromRoute(Name ="id")] int id)
        {
            if (ModelState.IsValid)
            {
                _manager.ProductService.DeleteOneProduct(id);
                TempData["success"] = $"{id} Product Removed";

                return RedirectToAction("Index");
            }
            return View();

        }



    }
}
