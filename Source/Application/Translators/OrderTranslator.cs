using System;
using System.Collections.Generic;
using System.Linq;
using Dto = CheckoutChallenge.DataContracts;
using Model = CheckoutChallenge.Domain.Model;

namespace CheckoutChallenge.Application.Translators
{
    internal static class OrderTranslator
    {
        public static IEnumerable<Dto.Order> ToDto(this IEnumerable<Model.Order> model)
        {
            return model.Select(ToDto);
        }

        public static Dto.Order ToDto(this Model.Order model)
        {
            return new Dto.Order
            {
                Id = model.Id
            };
        }
    }
}
