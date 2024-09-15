namespace WebApp.Models;
public class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public string ProductAlias { get; set; } = null!;
    public short CategoryId {get; set;}
    public string Unit {get; set;} = null!;
    public decimal Price { get; set; }
    public string Image {get; set;} = null!;
    public DateTime ProductDate{get; set;}
    public decimal SaleOff { get; set; }
    public int Viewed{get; set;}
    public string Description { get; set; } = null!;
    public string SupplierId { get; set; } = null!;
}
