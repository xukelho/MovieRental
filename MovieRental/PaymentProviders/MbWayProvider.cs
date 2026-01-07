using System;

namespace MovieRental.PaymentProviders
{
    public class MbWayProvider : IPaymentProvider
    {
        public bool CanHandle(string method)
        {
            if (string.IsNullOrWhiteSpace(method)) return false;
            var m = method.Trim().ToLowerInvariant();
            return m == "mbway" || m == "mb-way" || m == "mb_way";
        }

        public Task<bool> Pay(double price)
        {
            // ignore this implementation
            return Task.FromResult<bool>(true);
        }
    }
}
