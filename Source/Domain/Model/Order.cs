using System;
using System.Collections.Generic;

namespace CheckoutChallenge.Domain.Model
{
    public class Order
    {
        public Order(Guid id, Guid customerId)
        {
            Id = id;
            CustomerId = customerId;
            CreatedAt = DateTime.UtcNow;
            LastModifiedAt = CreatedAt;
        }

        public Guid Id { get; }

        public Guid CustomerId
        {
            get => customerId;
            set
            {
                if (value == Guid.Empty)
                    throw new DataValidationException("A valid customer id has to be specified.");

                customerId = value;
                LastModifiedAt = DateTime.UtcNow;
            }
        }
        private Guid customerId;

        public DateTime CreatedAt { get; }

        public DateTime LastModifiedAt { get; private set; }
    }
}
