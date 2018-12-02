using System;

namespace CheckoutChallenge.Domain.Model
{
    public class OrderItem
    {
        public OrderItem(int id, Guid productId, decimal amount)
        {
            Id = id;
            ProductId = productId;
            Amount = amount;
            CreatedAt = DateTime.UtcNow;
            LastModifiedAt = CreatedAt;
        }

        public int Id { get; }

        public Guid ProductId
        {
            get => productId;
            set
            {
                if (value == Guid.Empty)
                    throw new DataValidationException("A valid product id has to be specified.");

                productId = value;
                LastModifiedAt = DateTime.UtcNow;
            }
        }
        private Guid productId;

        public decimal Amount
        {
            get => amount;
            set
            {
                if (value <= 0)
                    throw new DataValidationException("The amount has to be positive number.");

                amount = value;
                LastModifiedAt = DateTime.UtcNow;
            }
        }
        private decimal amount;

        public DateTime CreatedAt { get; }

        public DateTime LastModifiedAt { get; private set; }
    }
}
