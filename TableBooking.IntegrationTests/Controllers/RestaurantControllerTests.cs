namespace TableBooking.IntegrationTests.Controllers;

using Microsoft.AspNetCore.Mvc.Testing;
using Model.Models;
using Newtonsoft.Json;

public class RestaurantControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public RestaurantControllerTests(WebApplicationFactory<Program> factory)
    {
            _factory = factory;
        }

    [Theory]
    [InlineData(1)]
    public async Task GetRestaurantById_WhenCalled_ReturnRestaurant(int id)
    {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"restaurant/{id}");
            Assert.True(response.IsSuccessStatusCode);

            var rawResponseData = await response.Content.ReadAsStringAsync();
            var restaurant = JsonConvert.DeserializeObject<Restaurant>(rawResponseData);

            Assert.NotNull(restaurant);
        }
}