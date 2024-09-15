using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;
public class CartController:BaseController{
    const string CartToken = "CartToken";
    public IActionResult Index(){
        string ? code = Request.Cookies[CartToken];
        if(string.IsNullOrEmpty(code)){
            return Redirect("/");
        }
        return View (Provider.Cart.GetCarts(code));
    }
    [HttpPost]
    public IActionResult Add(Cart obj){
        string ? code = Request.Cookies[CartToken];
        if(string.IsNullOrEmpty(code)){
            code = Helper.RandomString(32);
            Response.Cookies.Append(CartToken , code);
        }
        obj.CartCode = code;
        if(Provider.Cart.Add(obj) >0){
            return Redirect("/cart");
        }
        return Redirect("/cart/error");
    }
    public IActionResult Checkout()
    {
     return View();   
    }

    [HttpPost]
    public IActionResult Checkout(Invoice obj){
        int ret = Provider.Invoice.Add(obj);
        if(ret > 0){
            return Redirect($"/cart/invoice/{obj}.InvoiceId");
        }
        return View(obj);
    }
}