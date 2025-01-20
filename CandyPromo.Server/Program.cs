using CandyPromo.Server.Middlewares;
using CandyPromo.Server.ServiceCollectionExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.AddNpgsqlDbContext<CandyPromoContext>("postgres-data");
builder.Services
    .AddSwagger()
    .AddApiAuthentication(builder.Configuration)
    .AddServicesLayer()
    .AddHostedService<PrizeDrawHostedService>()
    .AddCors(options =>
    {
        options.AddPolicy(name: "Test",
                          policy =>
                          {
                              policy.WithOrigins("https://localhost:7200");
                          });
    });

var app = builder.Build();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:7200"));
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