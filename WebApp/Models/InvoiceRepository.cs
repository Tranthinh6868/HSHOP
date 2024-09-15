using System.Data;
using Dapper;

namespace WebApp.Models;

public class InvoiceRepository : BaseRepository{
    public InvoiceRepository(IDbConnection connection) : base(connection){

    }
    public int Add(Invoice obj){
        Random random = new Random();
        obj.InvoiceId = random.NextInt64(99999999, long .MaxValue);
        return connection.Execute("AddInvoice", obj, commandType: CommandType.StoredProcedure);
    }
}