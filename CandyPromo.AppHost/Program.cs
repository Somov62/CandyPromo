var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.CandyPromo_Server>("candypromo-server");

builder.AddNpmApp("react", "../candypromo.client")
    .WithReference(api)
    .WaitFor(api)
    .WithEnvironment("BROWSER", "none") // Disable opening browser on npm start
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

builder.Build().Run();