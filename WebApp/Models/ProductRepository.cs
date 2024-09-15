using System.Data;
using Dapper;

namespace WebApp.Models;
public class ProductRepository : BaseRepository{
    public ProductRepository(IDbConnection connection) : base(connection){

    }
    public IEnumerable<Product> GetProducts(){
        return connection.Query<Product>("SELECT * FROM Product");
    }
}