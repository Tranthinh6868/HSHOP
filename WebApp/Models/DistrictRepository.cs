using System.Data;
using Dapper;

namespace WebApp.Models;

public class DistrictRepository : BaseRepository{

    public DistrictRepository(IDbConnection connection) : base(connection){

    }

    public IEnumerable<District> GetDistricts(byte id){
        return connection.Query<District> ("SELECT * FROM District WHERE ProvinceId = @Id", new {id});
    }
}