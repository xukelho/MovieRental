using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.PaymentProviders
{
    public interface IPaymentProvider
    {
        // Return true if this provider handles the given method (case-insensitive).
        bool CanHandle(string method);

        // Process the payment. Return true if paid successfully.
        Task<bool> Pay(double price);
    }
}
