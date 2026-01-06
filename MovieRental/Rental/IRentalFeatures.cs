namespace MovieRental.Rental;

public interface IRentalFeatures
{
    Task<Rental> SaveAsync(Rental rental);
	IEnumerable<Rental> GetRentalsByCustomerName(string customerName);
}