using Microsoft.AspNetCore.Mvc;
using MovieRental.Rental;

namespace MovieRental.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RentalController : ControllerBase
    {

        private readonly IRentalFeatures _features;

        public RentalController(IRentalFeatures features)
        {
            _features = features;
        }


        [HttpPost]
        public IActionResult Post([FromBody] Rental.Rental rental)
        {
	        return Ok(_features.SaveAsync(rental));
        }

        [HttpGet("by-customer")]
        public IActionResult GetRentalsByCustomerName([FromQuery] string customerName)
        {
            if (string.IsNullOrWhiteSpace(customerName))
            {
                return BadRequest("customerName is required.");
            }

            var rentals = _features.GetRentalsByCustomerName(customerName);
            return Ok(rentals);
        }
    }
}
