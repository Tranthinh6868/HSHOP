namespace WebApp.Models;
public class InvoiceDetails{
    public string ProductId {get; set;} = null!;
    public string ProductName {get; set;} = null!;
    public string Image {get; set;} = null!;
    public decimal Price {get; set;}
    public short Quantity {get; set;}
}