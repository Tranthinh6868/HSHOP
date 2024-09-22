using System.Data;
using Dapper;

namespace WebApp.Models;
public class CartRepository : BaseRepository{
    public CartRepository(IDbConnection connection) : base (connection){

    }
    public int UpdateChecked(Cart obj){
        string sql= "UPDATE Cart SET Checked = ~Checked WHERE ProductId = @ProductId AND CartCode = @CartCode";
        return connection.Execute(sql, obj);
    }
    public int UpdateQuantity(Cart obj){
        string sql= "UPDATE Cart SET Quantity = @Quantity WHERE ProductId = @ProductId AND CartCode = @CartCode";
        return connection.Execute(sql, obj);
    }
    public int Add(Cart obj){
        return connection.Execute("AddCart", new{obj.CartCode, obj.Quantity, obj.ProductId}, commandType: CommandType.StoredProcedure);
    }
    public IEnumerable<Cart> GetCarts (string code){
        return connection.Query<Cart> ("SELECT Cart.*, Price, Image, ProductName FROM Cart JOIN Product ON Cart.ProductId = Product.ProductId AND CartCode = @Code", new {code} );

    }
    public IEnumerable<Cart> GetCartsChecked (string code){
        return connection.Query<Cart> ("SELECT Cart.*, Price, Image, ProductName FROM Cart JOIN Product ON Checked = 1 AND Cart.ProductId = Product.ProductId AND CartCode = @Code", new {code} );

    }
    public decimal GetAmount(string code){
        string sql = "SELECT SUM(Cart.Quantity * Product.Price) FROM Cart JOIN Product ON Cart.ProductId = Product.ProductId AND CartCode = @Code";
        return connection.ExecuteScalar<decimal>(sql, new{code});
    }
}