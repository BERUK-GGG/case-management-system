using Microsoft.AspNetCore.Mvc;
using RB_Ärendesystem.Entities;
using Affärslager;

namespace RB_CaseManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartsController : ControllerBase
    {
        private readonly NyReservdelController _nyReservdelController;
        private readonly TabellController _tabellController;

        public PartsController()
        {
            _nyReservdelController = new NyReservdelController();
            _tabellController = new TabellController();
        }

        /// <summary>
        /// Get all spare parts
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<Reservdel>> GetAllParts()
        {
            try
            {
                var parts = _tabellController.ReservdellTabell();
                return Ok(parts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Create a new spare part
        /// </summary>
        [HttpPost]
        public ActionResult<Reservdel> CreatePart([FromBody] Reservdel part)
        {
            try
            {
                if (part == null)
                {
                    return BadRequest("Part data is required");
                }

                _nyReservdelController.LäggTillReservdel(part);
                return CreatedAtAction(nameof(GetAllParts), new { id = part.ID }, part);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
