using CoreFlowAspire.ApiService.GraphQL;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddProblemDetails();

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// ✅ Add CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()  // หรือระบุเฉพาะ origin ที่ใช้เช่น http://localhost:7033
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

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
    .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true);

var app = builder.Build();

// ✅ Apply CORS
app.UseCors();

app.UseExceptionHandler();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
});

app.MapDefaultEndpoints();

app.Run();
