using Microsoft.AspNetCore.Mvc;
using RB_Ärendesystem.Entities;
using Affärslager;

namespace RB_CaseManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly NyKundController _nyKundController;
        private readonly UpdateraKundController _updateraKundController;
        private readonly TabellController _tabellController;

        public CustomersController()
        {
            _nyKundController = new NyKundController();
            _updateraKundController = new UpdateraKundController();
            _tabellController = new TabellController();
        }

        /// <summary>
        /// Get all customers
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<Kund>> GetAllCustomers()
        {
            try
            {
                var customers = _tabellController.KundTabell();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Create a new customer
        /// </summary>
        [HttpPost]
        public ActionResult<Kund> CreateCustomer([FromBody] Kund customer)
        {
            try
            {
                if (customer == null)
                {
                    return BadRequest("Customer data is required");
                }

                _nyKundController.LäggTillNyKund(customer);
                return CreatedAtAction(nameof(GetAllCustomers), new { id = customer.ID }, customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Update an existing customer
        /// </summary>
        [HttpPut("{id}")]
        public ActionResult UpdateCustomer(int id, [FromBody] Kund customer)
        {
            try
            {
                if (customer == null || customer.ID != id)
                {
                    return BadRequest("Invalid customer data");
                }

                _updateraKundController.UpdateraKund(customer);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
