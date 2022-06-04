using com.checkout.api;
using Microsoft.AspNetCore.Mvc.Testing;

namespace com.checkout.intergrationtests
{
    public class PaymentControllerIntegrationTests
    {

        private HttpClient _httpClient;

        public PaymentControllerIntegrationTests()
        {
            var webAppFactory = new WebApplicationFactory<Startup>();
            _httpClient = webAppFactory.CreateDefaultClient();
        }
        [Fact]
        public async Task DefaultRoute_ReturnsHelloWorld()
        {
           
        }
    }
}