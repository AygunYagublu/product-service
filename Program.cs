using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title       = "Product Service API",
        Version     = "v1",
        Description = "ASP.NET Core 8 ile yazilmish mehsul idareetme API-si"
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ProductRepository>();

var app = builder.Build();

/* database avtomatik yarat */
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

/* Swagger həmişə aktiv */
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product Service v1");
    c.RoutePrefix = string.Empty; /* ana səhifə Swagger olsun */
});

app.UseHttpsRedirection();
app.MapControllers();
app.Urls.Add("http://0.0.0.0:8080");
app.Run();