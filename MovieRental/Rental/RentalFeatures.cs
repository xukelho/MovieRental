using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieRental.Data;
using MovieRental.PaymentProviders;

namespace MovieRental.Rental
{
	public class RentalFeatures : IRentalFeatures
	{
		private readonly MovieRentalDbContext _movieRentalDb;
		private readonly IEnumerable<IPaymentProvider> _paymentProviders;

        const double pricePerDay = 5;//€

        public RentalFeatures(MovieRentalDbContext movieRentalDb, IEnumerable<IPaymentProvider> paymentProviders)
		{
			_movieRentalDb = movieRentalDb;
			_paymentProviders = paymentProviders;
		}

        // Async save
        // Advantage of async is that it frees up the thread to handle other requests while waiting for the database operation to complete.
        // Or a more obvious improvement, is that it allows the application to display a loading indicator while waiting for the save operation to finish, instead of freezing the UI, making the user scared that the application has crashed.
        public async Task<Rental> SaveAsync(Rental rental)
        {
            if (rental is null) throw new ArgumentNullException(nameof(rental));
            if (string.IsNullOrWhiteSpace(rental.PaymentMethod)) throw new ArgumentException("PaymentMethod is required.", nameof(rental.PaymentMethod));
            if (rental.DaysRented <= 0) throw new ArgumentException("DaysRented must be greater than zero.", nameof(rental.DaysRented));

            var price = rental.DaysRented * pricePerDay;

            var method = rental.PaymentMethod.Trim();
            var provider = _paymentProviders.FirstOrDefault(p => p.CanHandle(method));
            if (provider == null)
            {
                throw new InvalidOperationException($"Unsupported payment method: {rental.PaymentMethod}");
            }

            var paid = await provider.Pay(price);
            if (!paid)
            {
                throw new InvalidOperationException("Payment processing failed.");
            }

            _movieRentalDb.Rentals.Add(rental);
            await _movieRentalDb.SaveChangesAsync();
            return rental;
        }

        public IEnumerable<Rental> GetRentalsByCustomerName(string customerName)
		{
			var name = customerName?.Trim();
			if (string.IsNullOrEmpty(name))
			{
				return Enumerable.Empty<Rental>();
			}

			var lower = name.ToLowerInvariant();

			return _movieRentalDb.Rentals
				.Include(r => r.Movie)
				.Include(r => r.Customer)
				.Where(r => r.Customer != null && r.Customer.Name.ToLower().Contains(lower))
				.ToList();
		}
	}
}
