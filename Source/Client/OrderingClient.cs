using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using CheckoutChallenge.Client.Translators;
using CheckoutChallenge.DataContracts;

namespace CheckoutChallenge.Client
{
    public class OrderingClient : IOrderingClient
    {
        private const string ApiVersion = "v1";

        private readonly HttpClient httpClient;
        private readonly JsonSerializerSettings serializerSettings;
        private readonly JsonSerializer serializer;

        public OrderingClient(Uri serviceUrl)
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = serviceUrl;
            httpClient.Timeout = TimeSpan.FromSeconds(3);

            serializerSettings = new JsonSerializerSettings()
            {
                Converters = new List<JsonConverter> {new StringEnumConverter()},
                Formatting = Formatting.Indented
            };
            serializer = JsonSerializer.Create(serializerSettings);
        }

        public async Task<OrderList> GetOrders(CancellationToken cancellationToken)
        {
            try
            {
                var httpResponse = await httpClient.GetAsync($"{ApiVersion}/orders", cancellationToken);
                await HandleFailure(httpResponse, "Listing orders");

                var orderList = await DeserializeContent<DataContracts.OrderList>(httpResponse);
                return orderList.ToModel();
            }
            catch (HttpRequestException ex)
            {
                throw new OrderingClientException("An unexpected error occured while accessing ordering service.", ex);
            }
        }

        public Task<Order> GetOrder(Guid id, CancellationToken cancellationToken)
        {
            return GetOrder(new Uri($"{ApiVersion}/orders/{id}", UriKind.Relative), cancellationToken);
        }

        public async Task<Order> CreateOrder(Guid customerId, CancellationToken cancellationToken)
        {
            try
            {
                var orderToCreate = new DataContracts.Order
                {
                    CustomerId = customerId
                };

                var httpResponse = await httpClient.PostAsync(
                    $"{ApiVersion}/orders",
                    new StringContent(JsonConvert.SerializeObject(orderToCreate, serializerSettings), Encoding.UTF8, Constants.JsonMimeType),
                    cancellationToken);
                await HandleFailure(httpResponse, "Creating an order");

                var newOrderUrl = httpResponse.Headers.Location;
                return await GetOrder(newOrderUrl, cancellationToken);
            }
            catch (HttpRequestException ex)
            {
                throw new OrderingClientException("An unexpected error occured while accessing ordering service.", ex);
            }
        }

        private async Task<Order> GetOrder(Uri url, CancellationToken cancellationToken)
        {
            try
            {
                var httpResponse = await httpClient.GetAsync(url, cancellationToken);
                await HandleFailure(httpResponse, "Getting an order");

                var order = await DeserializeContent<DataContracts.Order>(httpResponse);
                return order.ToModel();
            }
            catch (HttpRequestException ex)
            {
                throw new OrderingClientException("An unexpected error occured while accessing ordering service.", ex);
            }
        }

        private async Task<T> DeserializeContent<T>(HttpResponseMessage httpResponse)
        {
            using (var streamReader = new StreamReader(await httpResponse.Content.ReadAsStreamAsync()))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                return serializer.Deserialize<T>(jsonReader);
            }
        }

        private async Task HandleFailure(HttpResponseMessage httpResponse, string operation)
        {
            if (httpResponse.IsSuccessStatusCode)
                return;

            Error error;
            try
            {
                error = await DeserializeContent<Error>(httpResponse);
            }
            catch (JsonException)
            {
                error = new Error("Failed to read error message.");
            }
            
            throw new OrderingClientException(
                $"{operation} failed because '{error.Message}'",
                httpResponse.StatusCode);
        }
    }
}
