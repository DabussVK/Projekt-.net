using ProductCatalog.Models;
using ProductCatalog.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ProductService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/products", (string? name, ProductService service) => {
    // Walidacja danych wejœciowych
    if (name is { Length: < 3 })
        return Results.BadRequest("Wpisz co najmiej 3 znaki");

    var products = service.GetAll(name);
    return Results.Ok(products);
});

app.Run();
