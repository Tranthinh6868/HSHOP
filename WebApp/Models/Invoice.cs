namespace WebApp.Models;
public class Invoice{
    
    public string CartCode {get;set;} = null!;
    public long InvoiceId {get; set;}
    public string Fullname {get; set;} = null!;
    public string Phone {get; set;} = null!;
    public string Email {get; set;} = null!;
    public string Address {get; set;} = null!;
    public int WardId {get; set;}
    public string WardName{get; set;} = null!;
    public string DistrictName{get; set;} = null!;
    public string ProvinceName{get; set;} = null!;
    public decimal Amount {get; set;}
    public DateTime InvoiceDate {get;set;}
    public IEnumerable<InvoiceDetails>? InvoiceDetails {get; set;}

}