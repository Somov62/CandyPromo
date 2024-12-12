using CandyPromo.Server.Middlewares;
using CandyPromo.Server.ServiceCollectionExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services
    .AddSwagger()
    .AddExceptionHandler<GlobalExceptionHandler>()
    .AddApiAuthentication(builder.Configuration)
    .AddServicesLayer()
    ;

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<RequestValidator>();

app.MapControllers();

app.Run();