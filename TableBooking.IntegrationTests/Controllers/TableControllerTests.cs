namespace TableBooking.IntegrationTests.Controllers;

using Microsoft.AspNetCore.Mvc.Testing;
using Model.Models;
using Newtonsoft.Json;

public class TableControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public TableControllerTests(WebApplicationFactory<Program> factory)
    {
            _factory = factory;
        }

    [Theory]
    [InlineData(1)]
    public async Task GetTableById_WhenCalled_ReturnTable(int id)
    {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"table/{id}");
            Assert.True(response.IsSuccessStatusCode);

            var rawResponseData = await response.Content.ReadAsStringAsync();
            var table = JsonConvert.DeserializeObject<Booking>(rawResponseData);

            Assert.NotNull(table);
        }
}