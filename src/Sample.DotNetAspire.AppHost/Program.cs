var builder = DistributedApplication.CreateBuilder(args);

//var cache = builder.AddRedis("cache");

var mongo = builder.AddMongoDB("mongo");
var mongoDb = mongo.AddDatabase("mongodb");

builder.AddProject<Projects.Sample_DotNetAspire_ApiService>("apiservice")
    .WithExternalHttpEndpoints()
    .WithReference(mongoDb)
    .WaitFor(mongoDb);

builder.Build().Run();
