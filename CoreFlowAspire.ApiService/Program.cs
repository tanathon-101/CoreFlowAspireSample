using CoreFlowAspire.ApiService.GraphQL;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddProblemDetails();
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddTransient<SqlConnection>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var connStr = config.GetConnectionString("coreflowdb");
    Console.WriteLine("✅ (DI) Connection String: " + connStr);
    return new SqlConnection(connStr);
});




builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true); // ✅ ใส่ตรงนี้


var app = builder.Build();

app.UseExceptionHandler();
app.UseRouting(); // ✅ ต้องมี ไม่งั้น GraphQL จะไม่ตอบ

app.UseEndpoints(endpoints => // ✅ สำคัญสำหรับ GraphQL v13
{
    endpoints.MapGraphQL();   // ✅ Map GraphQL
});

app.MapDefaultEndpoints();

app.Run();
