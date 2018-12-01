using System;
using System.Collections.Generic;
using System.Linq;

using Dto = CheckoutChallenge.DataContracts;
using Model = CheckoutChallenge.Client;

namespace CheckoutChallenge.Client.Translators
{
    internal static class OrderTranslator
    {
        public static Model.OrderList ToModel(this Dto.OrderList dto)
        {
            return new Model.OrderList(dto.Items.Select(ToModel));
        }
        public static Model.Order ToModel(this Dto.Order dto)
        {
            return new Model.Order(dto.Id);
        }
    }
}
