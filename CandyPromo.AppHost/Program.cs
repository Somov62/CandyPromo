var builder = DistributedApplication.CreateBuilder(args);

// ��������� �� Postgres SQL � ������ � ��� PgAdmin
var postgresData = builder.AddPostgres("postgres")
                          .WithPgAdmin()
                          .AddDatabase("postgres-data");

// ���������� ������ �������� ��
var migrationService = builder.AddProject<Projects.CandyPromo_Data_MigrationService>("data-migration-service")
        .WaitFor(postgresData)
       .WithReference(postgresData);

// ��������� Api
var api = builder.AddProject<Projects.CandyPromo_Server>("candypromo-server")
                 .WithReference(postgresData)
                 .WaitFor(migrationService);

// ��������� web �������
builder.AddNpmApp("react", "../candypromo.client")
       .WithReference(api)
       .WaitFor(api)
       .WithEnvironment("BROWSER", "none") // Disable opening browser on npm start
       .WithHttpEndpoint(env: "PORT")
       .WithExternalHttpEndpoints()
       .PublishAsDockerFile();

builder.Build().Run();