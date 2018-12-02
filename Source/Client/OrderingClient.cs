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
    public class OrderingClient : IOrderingClient, IInternalOrderingClient
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

                var createdOrder = await DeserializeContent<DataContracts.Order>(httpResponse);
                return createdOrder.ToModel(this);
            }
            catch (HttpRequestException ex)
            {
                throw new OrderingClientException("An unexpected error occured while accessing ordering service.", ex);
            }
        }

        public async Task<OrderList> FindOrders(CancellationToken cancellationToken)
        {
            try
            {
                var httpResponse = await httpClient.GetAsync($"{ApiVersion}/orders", cancellationToken);
                await HandleFailure(httpResponse, "Listing orders");

                var orderList = await DeserializeContent<DataContracts.OrderList>(httpResponse);
                return orderList.ToModel(this);
            }
            catch (HttpRequestException ex)
            {
                throw new OrderingClientException("An unexpected error occured while accessing ordering service.", ex);
            }
        }

        public async Task<Order> GetOrder(Uri id, CancellationToken cancellationToken)
        {
            try
            {
                var httpResponse = await httpClient.GetAsync(id, cancellationToken);
                await HandleFailure(httpResponse, $"Getting the order '{id}'");

                var order = await DeserializeContent<DataContracts.Order>(httpResponse);
                return order.ToModel(this);
            }
            catch (HttpRequestException ex)
            {
                throw new OrderingClientException("An unexpected error occured while accessing ordering service.", ex);
            }
        }

        public async Task<Order> StoreOrder(Order order, CancellationToken cancellationToken)
        {
            try
            {
                var httpResponse = await httpClient.PutAsync(
                    order.Id,
                    SerializeContent(order.ToDto()),
                    cancellationToken);
                await HandleFailure(httpResponse, $"Updating the order '{order.Id}'");

                var updatedOrder = await DeserializeContent<DataContracts.Order>(httpResponse);
                return updatedOrder.ToModel(this);
            }
            catch (HttpRequestException ex)
            {
                throw new OrderingClientException("An unexpected error occured while accessing ordering service.", ex);
            }
        }

        private HttpContent SerializeContent(object content)
        {
            return new StringContent(
                JsonConvert.SerializeObject(content, serializerSettings), 
                Encoding.UTF8,
                Constants.JsonMimeType);
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
