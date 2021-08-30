using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Utils.SberbankAcquiring.Models.Request;
using Utils.SberbankAcquiring.Models.Response;

namespace Utils.SberbankAcquiring
{
    public static class CloudPaymentsAPI
    {
        private static readonly Uri Host = new Uri("https://api.cloudpayments.ru/");

        public static async Task<RegisterDOResponse> Withdraw(RegisterDORequest request, string PublicApi, string password)
        {
            using (var client = new HttpClient())
            {
                string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(PublicApi + ":" + password));
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + svcCredentials);
                client.DefaultRequestHeaders.Add("Content-Type", "application/json");
                var content = JsonConvert.DeserializeObject<Dictionary<string, string>>(JsonConvert.SerializeObject(request));
                var uri = new Uri(Host, "payments/cards/topup");
                var response = await client.PostAsync(uri, new FormUrlEncodedContent(content));
                var responseContent = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<RegisterDOResponse>(responseContent);
            }
        }
    }
}
