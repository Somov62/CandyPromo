var builder = DistributedApplication.CreateBuilder(args);

var postgresData = builder.AddPostgres("postgres")
                          .WithPgWeb()
                          .AddDatabase("postgres-data");

builder.AddProject<Projects.CandyPromo_Data_MigrationService>("data-migration-service")
       .WithReference(postgresData);

var api = builder.AddProject<Projects.CandyPromo_Server>("candypromo-server")
                 .WaitFor(postgresData);

builder.AddNpmApp("react", "../candypromo.client")
       .WithReference(api)
       .WaitFor(api)
       .WithEnvironment("BROWSER", "none") // Disable opening browser on npm start
       .WithHttpEndpoint(env: "PORT")
       .WithExternalHttpEndpoints()
       .PublishAsDockerFile();

builder.Build().Run();