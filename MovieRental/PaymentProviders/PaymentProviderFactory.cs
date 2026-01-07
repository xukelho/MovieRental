using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieRental.PaymentProviders
{
    public class PaymentProviderFactory : IPaymentProviderFactory
    {
        private readonly IEnumerable<IPaymentProvider> _providers;

        public PaymentProviderFactory(IEnumerable<IPaymentProvider> providers)
        {
            _providers = providers ?? Enumerable.Empty<IPaymentProvider>();
        }

        public IPaymentProvider? GetProvider(string method)
        {
            if (string.IsNullOrWhiteSpace(method)) return null;
            // First provider that declares it can handle the method.
            return _providers.FirstOrDefault(p => p.CanHandle(method));
        }
    }
}
