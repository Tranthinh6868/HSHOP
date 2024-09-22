using System.Net;
using Azure.Core;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApp.Models;

namespace WebApp.Services;

public class VnPaymentService{
    //URL example:
    // https://sandbox.vnpayment.vn/paymentv2/vpcpay.html?vnp_Amount=1806000&vnp_Command=pay&vnp_CreateDate=20210801153333&vnp_CurrCode=VND&vnp_IpAddr=127.0.0.1&vnp_Locale=vn&vnp_OrderInfo=Thanh+toan+don+hang+%3A5&vnp_OrderType=other&vnp_ReturnUrl=https%3A%2F%2Fdomainmerchant.vn%2FReturnUrl&vnp_TmnCode=DEMOV210&vnp_TxnRef=5&vnp_Version=2.1.0&vnp_SecureHash=3e0d61a0c0534b2e36680b3f7277743e8784cc4e1d68fa7d276e79c23be7d6318d338b477910a27992f5057bb1582bd44bd82ae8009ffaf6d141219218625c42
    IHttpContextAccessor accessor;
    
    
    VnPayment vnPayment;
    public VnPaymentService(IOptions<VnPayment> options, IHttpContextAccessor accessor) 
        => (vnPayment, this.accessor) = (options.Value, accessor);
    //Lấy giá trị trong file appsetting.json ánh xạ vào các thành phần trong file Vnpayment.cs
    public string ToUrl(Invoice obj){
        IDictionary<string, string> dict = new SortedDictionary<string, string>(){
            {"vnp_Amount", (obj.Amount*100).ToString()},
            {"vnp_CreateDate", obj.InvoiceDate.ToString("yyyyMMddHHmmss")},
            {"vnp_Command", vnPayment.Command},
            {"vnp_CurrCode", vnPayment.CurrCode},
           // {"vnp_IpAddr", accessor.HttpContext.Connection.RemoteIpAddress.ToString()},
           
            {"vnp_OrderInfo", $"Payment for Invoice { obj.InvoiceId} with{obj.Amount}"},
            {"vnp_ReturnUrl", vnPayment.ReturnUrl},
            {"vnp_TmnCode", vnPayment.TmnCode},
            {"vnp_TxnRef", obj.InvoiceId.ToString()},
            {"vnp_Version", vnPayment.Version},
            {"vnp_Locale", vnPayment.Locale},
            {"vnp_OrderType", vnPayment.OrderType},

        };
        
        if(accessor.HttpContext != null && accessor.HttpContext.Connection.RemoteIpAddress != null){
            dict["vnp_IpAddr"] = accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            //dict["vnp_IpAddr"] = "127.0.0.1";
           //System.Console.WriteLine(dict["vnp_IpAddr"]);
           
        }
        string queryString  = string.Join("&", dict.Select(p=> $"{p.Key} = {WebUtility.UrlEncode(p.Value)}"));
        string url = vnPayment.Url + "?" + queryString + "&vnp_SecureHash=" + Helper.HmacSha512(queryString, vnPayment.HashSecret);
        return url;
    }
    
}

//https://sandbox.vnpayment.vn/paymentv2/vpcpay.html?vnp_Amount=76800000&vnp_Command=pay&vnp_CreateDate=20240915212616&vnp_CurrCode=VND&vnp_IpAddr=%3A%3A1&vnp_Locale=vn&vnp_OrderInfo=Payment+for+Invoice+2677053557140373213+with768000&vnp_OrderType=other&vnp_ReturnUrl=http%3A%2F%2Flocalhost%3A5263%2Fcart%2Fvnpaymentresponse&vnp_TmnCode=G0AV66P3&vnp_TxnRef=2677053557140373213&vnp_Version=2.1.0&vnp_SecureHash=41ED8CC2A2087D64FBA815CB5880A1E774B2071EAD508549AE6CE7B12FE144625B38155CFE8222FB35C392A93F6DB8AEB717352BC420EB5DFBC2C54155CF35BF
//https://sandbox.vnpayment.vn/paymentv2/vpcpay.html?vnp_Amount = 76800000&vnp_Command = pay&vnp_CreateDate = 20240915214114&vnp_CurrCode = VND&vnp_IpAddr = 127.0.0.1&vnp_Locale = vn&vnp_OrderInfo = Payment+for+Invoice+1247370670003495905+with768000&vnp_OrderType = other&vnp_ReturnUrl = http%3A%2F%2Flocalhost%3A5263%2Fcart%2Fvnpaymentresponse&vnp_TmnCode = G0NOWU5F&vnp_TxnRef = 1247370670003495905&vnp_Version = 2.1.0&vnp_SecureHash=85679006363409A320154AD2977E66A9F083827258C90C13BF0C10F8710EC58053741337FD04EB49ED89C12851AAB4792E0B43450AB4F83CE04B0F0438B70DC9