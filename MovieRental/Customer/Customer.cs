using System.ComponentModel.DataAnnotations;

namespace MovieRental.Customer
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        // Store the customer's name. Non-nullable with a default to avoid nulls in EF.
        public string Name { get; set; } = string.Empty;

        // Optional contact info
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}