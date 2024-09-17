using System.Data;
using Dapper;

namespace WebApp.Models;

public class InvoiceRepository : BaseRepository{
    public InvoiceRepository(IDbConnection connection) : base(connection){

    }
    public int Add(Invoice obj){
        Random random = new Random();
        obj.InvoiceId = random.NextInt64(99999999, long .MaxValue);
        obj.InvoiceDate = DateTime.Now;
        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@CartCode", obj.CartCode);
        parameters.Add("@InvoiceId", obj.InvoiceId);
        parameters.Add("@Fullname", obj.Fullname);
        parameters.Add("@Email", obj.Email);
        parameters.Add("@Address", obj.Address);
        parameters.Add("@Phone", obj.Phone);
        parameters.Add("@Amount", dbType: DbType.Decimal, direction: ParameterDirection.Output);

        int ret = connection.Execute("AddInvoice", parameters, commandType: CommandType.StoredProcedure);
        if(ret >0){
            obj.Amount = parameters.Get<decimal>("@Amount");
        }
        return ret;
    }
}