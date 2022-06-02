
using com.checkout.common;
using System.Text;
using System.Text.Json;

namespace com.checkout.api.Helpers
{
    public class WebRequests
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        internal async static Task ProcessTransactionAsync(UnprocessedTransaction transaction)
        {
            var serializedTransaction = JsonSerializer.Serialize(transaction);

            var requestContent = new StringContent(serializedTransaction, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("ProcessTransaction", requestContent);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var updatedTransaction = JsonSerializer.Deserialize<UnprocessedTransaction>(content);
        }
    }
}
