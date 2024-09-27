using System.ComponentModel.Design;
using System.Data;
using Dapper;

namespace WebApp.Models;

public class ProvinceRepository : BaseRepository{
    public ProvinceRepository(IDbConnection connection): base(connection){

    }

    public IEnumerable<Province> GetProvinces(){
        return connection.Query<Province>("SELECT * FROM Province");
    }
}