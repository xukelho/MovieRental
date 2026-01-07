using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.PaymentProviders
{
    public interface IPaymentProviderFactory
    {
        /// <summary>
        /// Returns a provider that can handle the specified payment method, or null if none found.
        /// </summary>
        IPaymentProvider? GetProvider(string method);
    }
}
