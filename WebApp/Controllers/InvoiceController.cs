using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;


public class InvoiceController : BaseController{
    public IActionResult Index(){
        return View();
    }

}