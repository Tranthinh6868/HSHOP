
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
public class HomeController : BaseController {
    public IActionResult Index(){
        ViewBag.Categories = Provider.Category.GetCategories();
        return View(Provider.Product.GetProducts());
    }

  
}