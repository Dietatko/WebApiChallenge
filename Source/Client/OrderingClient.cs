using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using CheckoutChallenge.Client.Translators;

namespace CheckoutChallenge.Client
{
    public class OrderingClient : IOrderingClient
    {
        private const string ApiVersion = "v1";

        private readonly HttpClient httpClient;
        private readonly JsonSerializer serializer;

        public OrderingClient(Uri serviceUrl)
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = serviceUrl;
            httpClient.Timeout = TimeSpan.FromSeconds(3);

            serializer = JsonSerializer.Create(new JsonSerializerSettings()
            {
                Converters = new List<JsonConverter> { new StringEnumConverter() },
                Formatting = Formatting.Indented
            });
        }

        public async Task<OrderList> GetOrders(CancellationToken cancellationToken)
        {
            DataContracts.OrderList result;
            try
            {
                var httpResponse = await httpClient.GetAsync($"{ApiVersion}/orders", cancellationToken);
                if (!httpResponse.IsSuccessStatusCode)
                    throw new OrderingClientException($"Listing orders failed with status code {httpResponse.StatusCode} and reason '{httpResponse.ReasonPhrase}'.");

                using(var streamReader = new StreamReader(await httpResponse.Content.ReadAsStreamAsync()))
                using (var jsonReader = new JsonTextReader(streamReader))
                {
                    result = serializer.Deserialize<DataContracts.OrderList>(jsonReader);
                }
            }
            catch (HttpRequestException ex)
            {
                throw new OrderingClientException("An unexpected error occured while accessing ordering service.", ex);
            }

            return result.ToModel();
        }
    }
}
