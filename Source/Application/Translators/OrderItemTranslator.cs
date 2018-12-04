using System;
using System.Collections.Generic;
using System.Linq;
using Dto = CheckoutChallenge.DataContracts;
using Model = CheckoutChallenge.Domain.Model;

namespace CheckoutChallenge.Application.Translators
{
    internal static class OrderItemTranslator
    {
        public static Dto.OrderItem ToDto(this Model.OrderItem model, Uri id)
        {
            return new Dto.OrderItem
            {
                Id = id,
                ProductId = model.ProductId,
                Amount = model.Amount,
                CreatedAt = model.CreatedAt,
                LastModifiedAt = model.LastModifiedAt
            };
        }
    }
}
