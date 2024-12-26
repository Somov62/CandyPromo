using CandyPromo.Server.Middlewares;
using CandyPromo.Server.ServiceCollectionExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services
    .AddSwagger()
    .AddApiAuthentication(builder.Configuration)
    .AddServicesLayer()
    .AddHostedService<PrizeDrawHostedService>()
    ;

builder.AddNpgsqlDbContext<CandyPromoContext>("postgres-data");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionHandler>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<RequestValidator>();

app.MapControllers();

app.Run();