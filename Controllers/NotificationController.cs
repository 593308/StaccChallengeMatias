using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StaccChallenge.Dtos;

namespace StaccChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NotificationController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves the notification of a user
        /// </summary>
        /// <param name="userId">userId for the user</param>
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetNotifications(int userId)
        {
            var notifications = await _context.Notifications
                .Where(n => n.UserId == userId)
                .Select(n => new NotificationDto
                {
                    Id = n.Id,
                    Content = n.Content,
                    DateCreated = n.DateCreated,
                    IsRead = n.IsRead,
                    Type = n.Type
                })
                .ToListAsync();

            return Ok(notifications);
        }
    }
}
