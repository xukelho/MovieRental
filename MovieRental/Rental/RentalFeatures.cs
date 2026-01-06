using Microsoft.EntityFrameworkCore;
using MovieRental.Data;

namespace MovieRental.Rental
{
	public class RentalFeatures : IRentalFeatures
	{
		private readonly MovieRentalDbContext _movieRentalDb;
		public RentalFeatures(MovieRentalDbContext movieRentalDb)
		{
			_movieRentalDb = movieRentalDb;
		}

        // Async save
        // Advantage of async is that it frees up the thread to handle other requests while waiting for the database operation to complete.
        // Or a more obvious improvement, is that it allows the application to display a loading indicator while waiting for the save operation to finish, instead of freezing the UI, making the user scared that the application has crashed.
        public async Task<Rental> SaveAsync(Rental rental)
        {
            _movieRentalDb.Rentals.Add(rental);
            await _movieRentalDb.SaveChangesAsync();
            return rental;
        }

        //TODO: finish this method and create an endpoint for it
        public IEnumerable<Rental> GetRentalsByCustomerName(string customerName)
		{
			return [];
		}

	}
}
