

using Dapper;
using Microsoft.Data.SqlClient;

namespace CoreFlowAspire.ApiService.GraphQL;

public class Query
{
     private readonly IConfiguration _config;

    public Query(IConfiguration config)
    {
        _config = config;
    }

    public async Task<IEnumerable<SampleData>> GetSampleDataAsync()
    {
        var connStr = _config.GetConnectionString("coreflowdb");
        using var conn = new SqlConnection(connStr);
        var sql = "SELECT Id, Name, CreatedAt FROM SampleData";
        return await conn.QueryAsync<SampleData>(sql);
    }
}

public class SampleData
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
}

