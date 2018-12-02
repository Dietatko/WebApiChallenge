using System;
using System.Collections.Generic;
using System.Linq;
using Dto = CheckoutChallenge.DataContracts;
using Model = CheckoutChallenge.Domain.Model;

namespace CheckoutChallenge.Application.Translators
{
    internal static class OrderTranslator
    {
        public static IEnumerable<Dto.Order> ToDto(this IEnumerable<Model.Order> model, Func<Model.Order, Uri> idProvider)
        {
            return model.Select(x => x.ToDto(idProvider(x)));
        }

        public static Dto.Order ToDto(this Model.Order model, Uri id)
        {
            return new Dto.Order
            {
                Id = id,
                CustomerId = model.CustomerId,
                CreatedAt = model.CreatedAt,
                LastModifiedAt = model.LastModifiedAt
            };
        }
    }
}
