using System.Data;
using Dapper;

namespace WebApp.Models;

public class WardRepository : BaseRepository{
    public WardRepository(IDbConnection connection) : base(connection){

    }
    public IEnumerable<Ward> GetWards(int id){
        return connection.Query<Ward> ("SELECT * FROM Ward WHERE DistrictId = @Id", new {id});
    }
}