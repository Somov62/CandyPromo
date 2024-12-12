var builder = DistributedApplication.CreateBuilder(args);

// Поднимаем БД Postgres SQL и вместе с ней PgAdmin
var postgresData = builder.AddPostgres("postgres")
                          .WithPgAdmin()
                          .AddDatabase("postgres-data");

// Пзапускаем сервис миграции БД
var migrationService = builder.AddProject<Projects.CandyPromo_Data_MigrationService>("data-migration-service")
        .WaitFor(postgresData)
       .WithReference(postgresData);

// Поднимаем Api
var api = builder.AddProject<Projects.CandyPromo_Server>("candypromo-server")
                 .WithReference(postgresData)
                 .WaitFor(migrationService);

// Поднимаем web клиента
builder.AddNpmApp("react", "../candypromo.client")
       .WithReference(api)
       .WaitFor(api)
       .WithEnvironment("BROWSER", "none") // Disable opening browser on npm start
       .WithHttpEndpoint(env: "PORT")
       .WithExternalHttpEndpoints()
       .PublishAsDockerFile();

builder.Build().Run();