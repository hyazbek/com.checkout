
using com.checkout.application.Interfaces;
using com.checkout.application.Models;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace com.checkout.application.services
{
    public class BankService : IBankService
    {
        public async Task<BankResponse> ProcessTranaction(UnprocessedTransaction transaction, string url)
        {

            var _httpClient = new HttpClient
            {
                Timeout = new TimeSpan(0, 5, 0)
            };
            var bankResponse = new BankResponse();
            using var client = _httpClient;

            var content = new StringContent(JsonConvert.SerializeObject(transaction), Encoding.UTF8, "application/json");

            using var response = client.PostAsync(url, content);

            var apiResponse = await response.Result.Content.ReadAsStringAsync();
            bankResponse = JsonConvert.DeserializeObject<BankResponse>(apiResponse);

            return bankResponse ?? new BankResponse();

        }
        
    }
}
