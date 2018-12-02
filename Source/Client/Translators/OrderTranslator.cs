using System;
using System.Collections.Generic;
using System.Linq;

using Dto = CheckoutChallenge.DataContracts;
using Model = CheckoutChallenge.Client;

namespace CheckoutChallenge.Client.Translators
{
    internal static class OrderTranslator
    {
        public static Model.OrderList ToModel(this Dto.OrderList dto, IInternalOrderingClient client)
        {
            return new Model.OrderList(dto.Items.Select(x => x.ToModel(client)));
        }

        public static Model.Order ToModel(this Dto.Order dto, IInternalOrderingClient client)
        {
            return new Model.Order(
                client,
                dto.Id,
                dto.CustomerId, 
                dto.CreatedAt, 
                dto.LastModifiedAt);
        }

        public static Dto.Order ToDto(this Model.Order model)
        {
            return new Dto.Order
            {
                Id = model.Id,
                CustomerId = model.CustomerId,
                CreatedAt = model.CreatedAt,
                LastModifiedAt = model.LastModifiedAt
            };
        }
    }
}
