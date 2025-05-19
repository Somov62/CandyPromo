var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.AddNpgsqlDbContext<CandyPromoContext>("postgres-data");
builder.Services
    .Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"))
    .AddSwagger()
    .AddOpenApi()
    .AddApiAuthentication(builder.Configuration)
    .AddServicesLayer()
    .AddHostedService<PrizeDrawHostedService>()
    .AddCors(options =>
    {
        options.AddPolicy(name: "Test",
            policy => policy.WithOrigins("https://localhost:7237"));
    });

var app = builder.Build();
app.UseCors(x =>
    x.AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins("https://localhost:7237"));
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionHandler>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<RequestValidator>();

app.MapControllers();

app.Run();