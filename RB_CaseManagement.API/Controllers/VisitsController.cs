using Microsoft.AspNetCore.Mvc;
using RB_Ärendesystem.Entities;
using Affärslager;

namespace RB_CaseManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VisitsController : ControllerBase
    {
        private readonly NyBokningController _nyBokningController;
        private readonly UppdateraBokningController _uppdateraBokningController;
        private readonly TabellController _tabellController;

        public VisitsController()
        {
            _nyBokningController = new NyBokningController();
            _uppdateraBokningController = new UppdateraBokningController();
            _tabellController = new TabellController();
        }

        /// <summary>
        /// Get all visits/appointments
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<Besök>> GetAllVisits()
        {
            try
            {
                var visits = _tabellController.BesökTabell();
                return Ok(visits);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Create a new visit/appointment
        /// </summary>
        [HttpPost]
        public ActionResult<Besök> CreateVisit([FromBody] Besök visit)
        {
            try
            {
                if (visit == null)
                {
                    return BadRequest("Visit data is required");
                }

                // Since AddBooking requires individual parameters, we'll call it with visit properties
                _nyBokningController.AddBooking(visit.Kund, visit.Mekaniker, visit.Syfte, visit.DateAndTime);
                return CreatedAtAction(nameof(GetAllVisits), new { id = visit.ID }, visit);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Update an existing visit/appointment
        /// </summary>
        [HttpPut("{id}")]
        public ActionResult UpdateVisit(int id, [FromBody] Besök visit)
        {
            try
            {
                if (visit == null || visit.ID != id)
                {
                    return BadRequest("Invalid visit data");
                }

                _uppdateraBokningController.UppdateraBokning(visit);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
