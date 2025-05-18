using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CoreFlowAspire.ApiService.GraphQL;

public class Mutation
{
    private readonly IConfiguration _configuration;

    public Mutation(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [GraphQLName("addSampleData")]
    public async Task<string> AddSampleData(string name)
    {
        var connectionString = _configuration.GetConnectionString("coreflowdb");

        await using var connection = new SqlConnection(connectionString);
        var sql = "INSERT INTO SampleData (Name, CreatedAt) VALUES (@Name, GETDATE())";

        var result = await connection.ExecuteAsync(sql, new { Name = name });

        return result > 0 ? "Insert successful" : "Insert failed";
    }
}
