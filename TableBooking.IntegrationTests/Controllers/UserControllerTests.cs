using Microsoft.AspNetCore.Mvc.Testing;

namespace TableBooking.IntegrationTests.Controllers
{

    public class UserControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public UserControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Login_WhenCalled_ReturnToken()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("login");
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Register_WhenCalled_ReturnSuccess()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("register");
            Assert.True(response.IsSuccessStatusCode);
        }
    }
}


