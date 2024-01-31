using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VirtuagymDevTask.Data;

namespace VirtuagymDevTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IGymRepository _repo;

        public UserController(IGymRepository repo)
        {
            _repo = repo;
        }

        // api/User     -- Get All Users
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _repo.GetAllUsers();
            return Ok(users);
        }

        // api/User/2     -- Get detail view of a single User
        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            var user = await _repo.GetUser(userId);
            if (user == null) return NotFound();
            return Ok(user);
        }
    }
}
