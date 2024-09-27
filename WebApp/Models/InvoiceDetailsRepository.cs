using System.Data;
using Dapper;

namespace WebApp.Models;

public class InvoiceDetailsRepository : BaseRepository{
    public InvoiceDetailsRepository(IDbConnection connection) : base (connection){

    }
    public IEnumerable<InvoiceDetails> GetInvoiceDetails(long id){
        string sql ="SELECT InvoiceDetails.*, ProductName, Image FROM InvoiceDetails JOIN Product ON InvoicedId = @Id AND InvoiceDetails.ProductId = Product.ProductId";
        return connection.Query<InvoiceDetails>(sql,new{id});
    }
}