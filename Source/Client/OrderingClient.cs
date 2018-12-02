using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public async Task<OrderItem> CreateOrderItem(Order order, Guid productId, decimal amount, CancellationToken cancellationToken)
        {
            try
            {
                var itemToCreate = new DataContracts.OrderItem
                {
                    ProductId = productId,
                    Amount = amount
                };

                var httpResponse = await httpClient.PostAsync(
                    AppendUri(order.Id, "items"),
                    new StringContent(JsonConvert.SerializeObject(itemToCreate, serializerSettings), Encoding.UTF8, Constants.JsonMimeType),
                    cancellationToken);
                await HandleFailure(httpResponse, "Creating an order item");

                var createdItem = await DeserializeContent<DataContracts.OrderItem>(httpResponse);
                return createdItem.ToModel(this);
            }
            catch (HttpRequestException ex)
            {
                throw new OrderingClientException("An unexpected error occured while accessing ordering service.", ex);
            }
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItems(Order order, CancellationToken cancellationToken)
        {
            try
            {
                var httpResponse = await httpClient.GetAsync(AppendUri(order.Id, "items"), cancellationToken);
                await HandleFailure(httpResponse, $"Getting items of the order '{order.Id}'");

                var items = await DeserializeContent<IEnumerable<DataContracts.OrderItem>>(httpResponse);
                return items.ToModel(this).ToList();
            }
            catch (HttpRequestException ex)
            {
                throw new OrderingClientException("An unexpected error occured while accessing ordering service.", ex);
            }
        }

        public async Task<OrderItem> GetOrderItem(Uri id, CancellationToken cancellationToken)
        {
            try
            {
                var httpResponse = await httpClient.GetAsync(id, cancellationToken);
                await HandleFailure(httpResponse, $"Getting the order item '{id}'");

                var order = await DeserializeContent<DataContracts.OrderItem>(httpResponse);
                return order.ToModel(this);
            }
            catch (HttpRequestException ex)
            {
                throw new OrderingClientException("An unexpected error occured while accessing ordering service.", ex);
            }
        }

        public async Task<OrderItem> StoreOrderItem(OrderItem item, CancellationToken cancellationToken)
        {
            try
            {
                var httpResponse = await httpClient.PutAsync(
                    item.Id,
                    SerializeContent(item.ToDto()),
                    cancellationToken);
                await HandleFailure(httpResponse, $"Updating the order item '{item.Id}'");

                var updatedOrder = await DeserializeContent<DataContracts.OrderItem>(httpResponse);
                return updatedOrder.ToModel(this);
            }
            catch (HttpRequestException ex)
            {
                throw new OrderingClientException("An unexpected error occured while accessing ordering service.", ex);
            }
        }

        public async Task DeleteOrderItem(OrderItem item, CancellationToken cancellationToken)
        {
            try
            {
                var httpResponse = await httpClient.DeleteAsync(
                    item.Id,
                    cancellationToken);
                await HandleFailure(httpResponse, $"Deleting the order item '{item.Id}'");
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

        private Uri AppendUri(Uri baseUri, string relativePath)
        {
            // TODO: find better way how to append relative uris
            return new Uri($"{baseUri}/{relativePath}", UriKind.Relative);
        }
    }
}
