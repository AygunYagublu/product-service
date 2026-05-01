using ProductService.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<ProductRepository>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();