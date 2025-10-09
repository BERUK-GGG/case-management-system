using Microsoft.AspNetCore.Mvc;
using Entities;
using Affärslager;

namespace RB_CaseManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JournalsController : ControllerBase
    {
        private readonly NyJournalController _nyJournalController;
        private readonly TabellController _tabellController;

        public JournalsController()
        {
            _nyJournalController = new NyJournalController();
            _tabellController = new TabellController();
        }

        /// <summary>
        /// Get all journals
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<Journal>> GetAllJournals()
        {
            try
            {
                var journals = _tabellController.JournalTabell();
                return Ok(journals);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Create a new journal entry
        /// </summary>
        [HttpPost]
        public ActionResult<Journal> CreateJournal([FromBody] Journal journal)
        {
            try
            {
                if (journal == null)
                {
                    return BadRequest("Journal data is required");
                }

                // Using AddJournal method with the required parameters
                _nyJournalController.AddJournal(journal.Åtgärder, journal.Besök, journal.reservdelar.ToList());
                return CreatedAtAction(nameof(GetAllJournals), new { id = journal.ID }, journal);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
