using com.checkout.api;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using com.checkout.api.Helpers;
using com.checkout.data.Model;
using System.Text.Json;
using System.Text;

namespace com.checkout.intergrationtests
{
    public class PaymentControllerIntegrationTests : IClassFixture<TestingWebAppFactory<Startup>>
    {

        private HttpClient _httpClient;

        public PaymentControllerIntegrationTests()
        {
            var webAppFactory = new WebApplicationFactory<Startup>();
            _httpClient = webAppFactory.CreateDefaultClient();
        }
        [Fact]
        public async Task Get_WhenCalled_ReturnsAllCards()
        {
            var response = await _httpClient.GetAsync("/Payment/GetAllCards");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("123456789", responseString);
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsTransactionByID()
        {
            var response = await _httpClient.GetAsync("/Payment/GetTransactionByID?transactionId=ED9A5B76-B5CC-46F6-9372-7657A2812158");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("322", responseString);
            
        }

        [Fact]
        public async Task Post_SendForm_InsertDetailsInDatabase()
        {
            var paymentRequest = new PaymentRequest()
            {
                Amount = 444,
                Card = new CardDetails() { CardDetailsID = 11, CardNumber = "5555555555", Cvv = "555", ExpiryMonth = "11", ExpiryYear = "2055", HolderName = "Unit Testing" },
                CurrencyID = 3,
                MerchantID = "1C4352E9-BEB6-4C7F-8BFC-9263DE60238B"
            };

            var requestContent = JsonSerializer.Serialize(paymentRequest);
            
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Payment/ProcessTransaction");
            postRequest.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            postRequest.Content = new StringContent(requestContent, Encoding.UTF8);
            postRequest.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("British Pound", responseString);
        }

        [Fact]
        public async Task Post_SendForm_WrongCurrencyReturnBadRequest()
        {
            var paymentRequest = new PaymentRequest()
            {
                Amount = 444,
                Card = new CardDetails() { CardDetailsID = 11, CardNumber = "5555555555", Cvv = "555", ExpiryMonth = "11", ExpiryYear = "2055", HolderName = "Unit Testing" },
                CurrencyID = 5,
                MerchantID = "1C4352E9-BEB6-4C7F-8BFC-9263DE60238B"
            };

            var requestContent = JsonSerializer.Serialize(paymentRequest);
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Payment/ProcessTransaction");
            postRequest.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            postRequest.Content = new StringContent(requestContent, Encoding.UTF8);
            postRequest.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(postRequest);
            
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("Invalid Currency", responseString);
        }
        [Fact]
        public async Task Post_SendForm_WrongMerchantReturnBadRequest()
        {
            var paymentRequest = new PaymentRequest()
            {
                Amount = 444,
                Card = new CardDetails() { CardDetailsID = 11, CardNumber = "5555555555", Cvv = "555", ExpiryMonth = "11", ExpiryYear = "2055", HolderName = "Unit Testing" },
                CurrencyID = 2,
                MerchantID = "BCD61F3D-6B23-4FE1-927B-FAA08A4B8908"
            };

            var requestContent = JsonSerializer.Serialize(paymentRequest);
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Payment/ProcessTransaction");
            postRequest.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            postRequest.Content = new StringContent(requestContent, Encoding.UTF8);
            postRequest.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(postRequest);
            
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("Invalid Merchant", responseString);
        }
    }
}