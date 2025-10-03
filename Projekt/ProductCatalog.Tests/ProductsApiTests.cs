using Microsoft.AspNetCore.Mvc.Testing;
using ProductCatalog.Models;
using System.Net;
using System.Net.Http.Json;

public class ProductsApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public ProductsApiTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetProducts_WithoutName_ReturnsAllProducts()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/products");

        // Assert
        response.EnsureSuccessStatusCode(); // Status 200 OK
        var products = await response.Content.ReadFromJsonAsync<Product[]>();
        Assert.Equal(3, products!.Length); // Sprawdzamy, że zwrócił wszystkie produkty
    }

    [Fact]
    public async Task GetProducts_WithShortName_ReturnsBadRequest()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/products?name=ab"); // krótsze niż 3 znaki

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode); // 400 BadRequest
    }

    [Fact]
    public async Task GetProducts_WithNameFilter_ReturnsFilteredProduct()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/products?name=Lap"); // filtr "Lap"

        response.EnsureSuccessStatusCode(); // Status 200 OK
        var products = await response.Content.ReadFromJsonAsync<Product[]>();
        Assert.NotNull(products); // Sprawdzenie, czy products nie jest null
        Assert.Single(products); // Jeden produkt pasuje
        Assert.Equal("Laptop", products[0].Name); // Sprawdzenie, czy to "Laptop"
    }
}
