using System;

namespace CheckoutChallenge.DataContracts
{
    public class Order
    {
        public Uri Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModifiedAt { get; set; }
    }
}
