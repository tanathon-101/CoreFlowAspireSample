using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);


builder.AddProject<Projects.CoreFlowAspire_ApiService>("apiservice");
builder.AddProject<Projects.CoreFlowAspire_Web>("web");



builder.Build().Run();
