using System.Data;
using Dapper;

namespace WebApp.Models;
public class CartRepository : BaseRepository{
    public CartRepository(IDbConnection connection) : base (connection){

    }
    public int Add(Cart obj){
        return connection.Execute("AddCart", new{obj.CartCode, obj.Quantity, obj.ProductId}, commandType: CommandType.StoredProcedure);
    }
    public IEnumerable<Cart> GetCarts (string code){
        return connection.Query<Cart> ("SELECT Cart.*, Price, Image, ProductName FROM Cart JOIN Product ON Cart.ProductId = Product.ProductId AND CartCode = @Code", new {code} );

    }
}