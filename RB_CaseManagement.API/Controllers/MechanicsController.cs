using Microsoft.AspNetCore.Mvc;
using RB_Ärendesystem.Entities;
using Affärslager;

namespace RB_CaseManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MechanicsController : ControllerBase
    {
        private readonly TabellController _tabellController;

        public MechanicsController()
        {
            _tabellController = new TabellController();
        }

        /// <summary>
        /// Get all mechanics
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<Mekaniker>> GetAllMechanics()
        {
            try
            {
                var mechanics = _tabellController.MekanikerTabell();
                return Ok(mechanics);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
