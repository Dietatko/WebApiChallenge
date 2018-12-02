using System;

namespace CheckoutChallenge.DataContracts
{
    public class OrderItem
    {
        public Uri Id { get; set; }
        public Guid ProductId { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModifiedAt { get; set; }
    }
}
