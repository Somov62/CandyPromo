var builder = DistributedApplication.CreateBuilder(args);

// ÐÐ¾Ð´Ð½Ð¸Ð¼Ð°ÐµÐ¼ ÐÐ Postgres SQL Ð¸ Ð²Ð¼ÐµÑÑÐµ Ñ Ð½ÐµÐ¹ PgAdmin
var postgresData = builder.AddPostgres("postgres")
    .WithPgAdmin()
    .AddDatabase("postgres-data");

// ÐÐ·Ð°Ð¿ÑÑÐºÐ°ÐµÐ¼ ÑÐµÑÐ²Ð¸Ñ Ð¼Ð¸Ð³ÑÐ°ÑÐ¸Ð¸ ÐÐ
var migrationService = builder.AddProject<Projects.CandyPromo_Data_MigrationService>("data-migration-service")
    .WaitFor(postgresData)
    .WithReference(postgresData);

// ÐÐ¾Ð´Ð½Ð¸Ð¼Ð°ÐµÐ¼ Api
var api = builder
    .AddProject<Projects.CandyPromo_Server>("candypromo-server", launchProfileName: "http")
    .WithReference(postgresData)
    .WaitFor(migrationService);

// ÐÐ¾Ð´Ð½Ð¸Ð¼Ð°ÐµÐ¼ web ÐºÐ»Ð¸ÐµÐ½ÑÐ°
builder.
    AddNpmApp("react", "../candypromo.client", scriptName: "dev")
    .WithReference(api)
    .WaitFor(api)
    .WithEnvironment("BROWSER", "none") // Disable opening browser on npm start
    .WithHttpEndpoint(env: "PORT", targetPort: 7237)
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

builder.Build().Run();