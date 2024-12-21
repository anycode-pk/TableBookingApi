using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using TableBooking.Model.Models;

namespace TableBooking.IntegrationTests.Controllers
{

    public class BookingControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public BookingControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData(1)]
        public async Task GetBookingById_WhenCalled_ReturnBooking(int id)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"booking/{id}");
            Assert.True(response.IsSuccessStatusCode);

            var rawResponseData = await response.Content.ReadAsStringAsync();
            var booking = JsonConvert.DeserializeObject<Booking>(rawResponseData);

            Assert.NotNull(booking);
        }
    }
}

