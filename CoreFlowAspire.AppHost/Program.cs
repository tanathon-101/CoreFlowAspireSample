var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.CoreFlowAspire_Web>("webapi");

builder.Build().Run();
