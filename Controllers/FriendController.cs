using Microsoft.AspNetCore.Mvc;

namespace StaccChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendController : ControllerBase
    {

        private readonly AppDbContext _context;
        public FriendController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("send-friend-request/{requestedID}")]
        public async Task<IActionResult> SendFriendRequest(int requestedId)
        {
            throw new NotImplementedException();
        }

        [HttpPost("accept-friend-request/{requestedID}")]
        public async Task<IActionResult> AcceptFriendRequest(int requestId)
        {
            throw new NotImplementedException();
        }

        [HttpPost("decline-friend-request/{requestId}")]
        public async Task<IActionResult> DeclineFriendRequest(int requestId)
        {
            throw new NotImplementedException();
        }

        [HttpGet("get-friends")]
        public async Task<IActionResult> GetFriends()
        {
            throw new NotImplementedException();
        }
    }
}
