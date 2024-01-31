using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VirtuagymDevTask.Services;

namespace VirtuagymDevTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckinController : ControllerBase
    {
        private readonly ICheckInService _checkin;

        public CheckinController(ICheckInService checkin)
        {
            _checkin = checkin;
        }

        // api/Checkin/2        -- User Checkin endpoint
        [HttpGet("{userId}")]
        public async Task<IActionResult> UserChekin(int userId)
        {
           var user = await _checkin.CheckUserValidity(userId);
           if (user == null) return NotFound();
            switch (user.ContactNo)
            {
                case 0: return BadRequest(new { message = "User doesn't have a membership to checkin" });
                case 1: return BadRequest(new { message = "Membership Canceled! you can't checkin!" });
                case 2: return BadRequest(new { message = "Insufficient credits! you can't checkin!" });
                case 3: return BadRequest(new { message = "Something went wrong when adding Invoice lines" });
                default:
                    return Ok(user);
            }
        }
    }
}
