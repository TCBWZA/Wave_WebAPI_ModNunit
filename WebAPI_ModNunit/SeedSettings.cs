namespace WebAPI_ModNunit
{
    /// <summary>
    /// Configuration settings for database seeding.
    /// Provides validated configuration for generating realistic test data.
    /// </summary>
    public class SeedSettings
    {
        /// <summary>
        /// Gets or sets whether database seeding is enabled.
        /// </summary>
        public bool EnableSeeding { get; set; } = true;

        /// <summary>
        /// Gets or sets the number of customers to seed.
        /// Must be greater than 0.
        /// </summary>
        public int CustomerCount { get; set; } = 20;

        /// <summary>
        /// Gets or sets the number of products to seed.
        /// Must be greater than 0.
        /// </summary>
        public int ProductCount { get; set; } = 20;

        /// <summary>
        /// Gets or sets the number of orders to seed.
        /// Must be greater than 0.
        /// </summary>
        public int OrderCount { get; set; } = 50;

        /// <summary>
        /// Gets or sets the minimum number of phone numbers per customer.
        /// Must be >= 0 and less than or equal to MaxPhoneNumbersPerCustomer.
        /// </summary>
        public int MinPhoneNumbersPerCustomer { get; set; } = 1;

        /// <summary>
        /// Gets or sets the maximum number of phone numbers per customer.
        /// Must be >= MinPhoneNumbersPerCustomer.
        /// </summary>
        public int MaxPhoneNumbersPerCustomer { get; set; } = 3;

        /// <summary>
        /// Gets or sets the minimum number of order items per order.
        /// Must be >= 1.
        /// </summary>
        public int MinOrderItemsPerOrder { get; set; } = 1;

        /// <summary>
        /// Gets or sets the maximum number of order items per order.
        /// Must be >= MinOrderItemsPerOrder.
        /// </summary>
        public int MaxOrderItemsPerOrder { get; set; } = 5;

        /// <summary>
        /// Validates the seed settings to ensure all values are within acceptable ranges.
        /// Throws ArgumentException if any settings are invalid.
        /// </summary>
        public void Validate()
        {
            if (CustomerCount <= 0)
                throw new ArgumentException("CustomerCount must be greater than 0.", nameof(CustomerCount));

            if (ProductCount <= 0)
                throw new ArgumentException("ProductCount must be greater than 0.", nameof(ProductCount));

            if (OrderCount <= 0)
                throw new ArgumentException("OrderCount must be greater than 0.", nameof(OrderCount));

            if (MinPhoneNumbersPerCustomer < 0)
                throw new ArgumentException("MinPhoneNumbersPerCustomer must be >= 0.", nameof(MinPhoneNumbersPerCustomer));

            if (MaxPhoneNumbersPerCustomer < MinPhoneNumbersPerCustomer)
                throw new ArgumentException("MaxPhoneNumbersPerCustomer must be >= MinPhoneNumbersPerCustomer.", nameof(MaxPhoneNumbersPerCustomer));

            if (MinOrderItemsPerOrder < 1)
                throw new ArgumentException("MinOrderItemsPerOrder must be >= 1.", nameof(MinOrderItemsPerOrder));

            if (MaxOrderItemsPerOrder < MinOrderItemsPerOrder)
                throw new ArgumentException("MaxOrderItemsPerOrder must be >= MinOrderItemsPerOrder.", nameof(MaxOrderItemsPerOrder));
        }
    }
}

