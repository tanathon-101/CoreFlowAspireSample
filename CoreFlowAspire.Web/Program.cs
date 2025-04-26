using Dapper;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("coreflowdb");
builder.Services.AddScoped(_ => new SqlConnection(connectionString));

var app = builder.Build();

app.MapPost("/publish", async (SqlConnection db, RequestData data) =>
{
    var sql = "INSERT INTO SampleData (Name, CreatedAt) VALUES (@Name, GETDATE())";
    await db.ExecuteAsync(sql, new { data.Name });

    return Results.Ok("Insert success");
});

app.Run();

record RequestData(string Name);
