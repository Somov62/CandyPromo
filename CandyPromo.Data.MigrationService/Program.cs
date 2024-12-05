var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<Worker>();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource("Data.MigrrationService"));

builder.AddNpgsqlDbContext<CandyPromoContext>("sqldata");

var host = builder.Build();
host.Run();
