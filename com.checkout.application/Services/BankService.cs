
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
        public async Task<BankResponse> ProcessTranaction(UnprocessedTransaction transaction)
        {
           
            var _httpClient = new HttpClient
            {
                Timeout = new TimeSpan(0, 5, 0)
            };
            var bankResponse = new BankResponse();
            using(var client = _httpClient) 
            {                
                StringContent content = new StringContent(JsonConvert.SerializeObject(transaction), Encoding.UTF8, "application/json");
            //using (var response = client.PostAsync("https://comcheckoutbank.azurewebsites.net/BankTransaction/ProcessTransaction", content))
                using (var response = client.PostAsync("http://localhost:5074/BankTransaction/ProcessTransaction", content))
                {
                    string apiResponse = await response.Result.Content.ReadAsStringAsync();
                    bankResponse = JsonConvert.DeserializeObject<BankResponse>(apiResponse);
                }
            }

            return bankResponse != null? bankResponse : new BankResponse();
            
        }
    }
}
