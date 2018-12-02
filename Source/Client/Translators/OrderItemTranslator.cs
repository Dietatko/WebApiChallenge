using System.Collections.Generic;
using System.Linq;

using Dto = CheckoutChallenge.DataContracts;
using Model = CheckoutChallenge.Client;

namespace CheckoutChallenge.Client.Translators
{
    internal static class OrderItemTranslator
    {
        public static IEnumerable<Model.OrderItem> ToModel(this IEnumerable<Dto.OrderItem> dtos, IInternalOrderingClient client)
        {
            return dtos.Select(x => x.ToModel(client));
        }

        public static Model.OrderItem ToModel(this Dto.OrderItem dto, IInternalOrderingClient client)
        {
            return new Model.OrderItem(
                client,
                dto.Id,
                dto.ProductId,
                dto.Amount,
                dto.CreatedAt, 
                dto.LastModifiedAt);
        }

        public static Dto.OrderItem ToDto(this Model.OrderItem model)
        {
            return new Dto.OrderItem
            {
                Id = model.Id,
                ProductId = model.ProductId,
                Amount = model.Amount,
                CreatedAt = model.CreatedAt,
                LastModifiedAt = model.LastModifiedAt
            };
        }
    }
}
