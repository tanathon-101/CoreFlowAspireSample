using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CoreFlowAspire.ApiService.GraphQL;

public class Query
{
    private readonly IConfiguration _config;

    public Query(IConfiguration config)
    {
        _config = config;
    }

    [GraphQLDescription("ดึงข้อมูลทั้งหมดจาก SampleData")]
    public async Task<IEnumerable<SampleData>> GetSampleDataAsync()
    {
        var connStr = _config.GetConnectionString("coreflowdb");
        await using var conn = new SqlConnection(connStr);
        var sql = "SELECT  Id, Name, CreatedAt FROM SampleData ORDER BY CreatedAt asc";
        return await conn.QueryAsync<SampleData>(sql);
    }
}

public class SampleData
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
}
