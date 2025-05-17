using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);


builder.AddProject<Projects.CoreFlowAspire_ApiService>("apiservice");


builder.Build().Run();
