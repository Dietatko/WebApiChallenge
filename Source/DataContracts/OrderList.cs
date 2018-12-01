using System;
using System.Collections.Generic;

namespace CheckoutChallenge.DataContracts
{
    /// <summary>
    /// A set of orders.
    /// Using specialized object instead of simple array allows future enhancements to add information about total amount of orders (if subset is returned) and paging information.
    /// </summary>
    public class OrderList
    {
        public IEnumerable<Order> Items { get; set; }
    }
}
