using System;

namespace MovieRental.PaymentProviders
{
    public class PayPalProvider : IPaymentProvider
    {
        public bool CanHandle(string method) =>
            string.Equals(method?.Trim(), "paypal", StringComparison.OrdinalIgnoreCase);

        public Task<bool> Pay(double price)
        {
            // ignore this implementation
            return Task.FromResult<bool>(true);
        }
    }
}
