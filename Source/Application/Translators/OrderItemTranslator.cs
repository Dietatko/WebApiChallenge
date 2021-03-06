﻿using System;
using System.Collections.Generic;
using System.Linq;
using Dto = CheckoutChallenge.DataContracts;
using Model = CheckoutChallenge.Domain.Model;

namespace CheckoutChallenge.Application.Translators
{
    internal static class OrderItemTranslator
    {
        public static IEnumerable<Dto.OrderItem> ToDto(this IEnumerable<Model.OrderItem> model, Func<Model.OrderItem, Uri> idProvider)
        {
            return model.Select(x => x.ToDto(idProvider(x)));
        }

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
