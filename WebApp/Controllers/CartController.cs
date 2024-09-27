using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers;
public class CartController:BaseController{
    const string CartToken = "CartToken";
    VnPaymentService paymentService;
    public CartController(VnPaymentService paymentService)  => this.paymentService = paymentService;

    public IActionResult Check(Cart obj){
        //return Json(obj);
        string ?code = Request.Cookies[CartToken];
        if(string.IsNullOrEmpty(code)){
            return Json(0);
        }
        obj.CartCode = code; 
        return Json(Provider.Cart.UpdateChecked(obj));
    }
    
    public IActionResult Invoice(long id){
        // Invoice? obj = Provider.Invoice.GetInvoice(id);
        // if(obj != null){
        //     obj.InvoiceDetails = Provider.InvoiceDetailsRepository.GetInvoiceDetails(id);
        //     return View(obj);
        // }
        Invoice? obj = Provider.Invoice.GetInvoiceWithDetails(id);
        if(obj != null){
            return View(obj);
        }

        return Redirect("/");
    }
    public IActionResult Edit(Cart obj){
        //return Json(obj);
        string ?code = Request.Cookies[CartToken];
        if(string.IsNullOrEmpty(code)){
            return Json(0);
        }
        obj.CartCode = code; 
        return Json(Provider.Cart.UpdateQuantity(obj));
    }

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
    [HttpPost]
    public IActionResult District(byte id){
        return Json(Provider.District.GetDistricts(id));
    }
    [HttpPost]
    public IActionResult Ward(int id){
        return Json(Provider.Ward.GetWards(id));
    }
    public IActionResult Checkout(){
        string? code = Request.Cookies[CartToken];
        if(string.IsNullOrEmpty(code)){
            return Redirect("/");
        }
        ViewBag.Provinces = new SelectList( Provider.Province.GetProvinces() , "ProvinceId", "ProvinceName");
        IEnumerable<Cart> carts = Provider.Cart.GetCartsChecked(code);
        ViewBag.Carts = carts;
        ViewBag.Total = carts.Sum(p=> p.Price * p.Quantity);
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
            return Redirect($"/cart/invoice/{obj.InvoiceId}");
        }
        return View(obj);
    }
}

