using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers;
public class CartController:BaseController{
    const string CartToken = "CartToken";
    VnPaymentService paymentService;
    public CartController(VnPaymentService paymentService)  => this.paymentService = paymentService;
    public IActionResult VnPayment(Invoice obj){
        string? code = Request.Cookies[CartToken];
        if(string.IsNullOrEmpty(code)){
            return Redirect("/");
        }
        obj.CartCode = code;
        int ret = Provider.Invoice.Add(obj);
        if(ret>0){
            //return Redirect($"/cart/invoice/{obj.InvoiceId}");
           // obj.Amount = Provider.Cart.GetAmount(code) *1000;
            obj.Amount = obj.Amount * 1000;
            string url = paymentService.ToUrl(obj);  
            System.Console.WriteLine("***************************");
            System.Console.WriteLine(url);
            return Redirect(url);
        }
        //Random random = new Random();
        // Invoice obj = new Invoice{
        //     InvoiceId = random.NextInt64(99999, long.MaxValue),
        //     Amount = Provider.Cart.GetAmount(code) *1000,
        //     InvoiceDate = DateTime.Now
        // };
        TempData["Msg"] = "Check Failed";
        return Redirect("/cart/checkout");
       
    }

    public IActionResult VnPaymentResponse(VnPaymentResponse obj){
        //return Json(obj);
        int ret = Provider.VnPayment.Add(obj);
        return Redirect("/cart/success");
    }
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
        string? code = Request.Cookies[CartToken];
        if(string.IsNullOrEmpty(code)){
            return Redirect("/");
        }
        obj.CartCode = code;
        int ret = Provider.Invoice.Add(obj);
        if(ret > 0){
            return Redirect($"/cart/invoice/{obj}.InvoiceId");
        }
        return View(obj);
    }
}

