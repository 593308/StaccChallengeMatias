using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StaccChallenge.Models;

namespace StaccChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SavingsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SavingsController(AppDbContext context)
        {
            // Database context
            _context = context;
        }


        // Endpoint to add a new saving for a user
        [HttpPost("add-saving")]
        public async Task<IActionResult> AddSaving(decimal amount)
        {
            // Hardcoded userId
            var userId = 1;

            // Fetch the total savings for the user
            var totalSavings = await _context.TotalSavings.FirstOrDefaultAsync(ts => ts.UserId == userId);

            // If the user does not have total savings, create a new one
            if (totalSavings == null)
            {
                totalSavings = new TotalSavings { UserId = userId, TotalAmount = amount };
                _context.TotalSavings.Add(totalSavings);
            }
            else
            {
                totalSavings.TotalAmount += amount;
            }

            // creates a new saving object
            var saving = new Saving
            {
                UserId = userId,
                Amount = amount,
                DateSaved = DateTime.Now,

            };

            // adds the savings to the context
            _context.Savings.Add(saving);

            // Updates the database
            await _context.SaveChangesAsync();

            // Checks if the user has reached any milestone
            await CheckForMilestones(userId);

            return Ok(saving);
        }

        /// <summary>
        /// Checks if user has reached any savings milestones, sends a notification if it has
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private async Task CheckForMilestones(int userId)
        {
            var totalSavings = await _context.TotalSavings.FirstOrDefaultAsync(ts => ts.UserId == userId);
            var milestones = await _context.Milestones.Where(m => m.Amount <= totalSavings.TotalAmount).ToListAsync();

            foreach (var milestone in milestones)
            {
                // checks if user already has a notification
                var existingNotification = await _context.Notifications.FirstOrDefaultAsync(n => n.UserId == userId && n.MilestoneId == milestone.Id);

                if (existingNotification == null)
                {
                    // creates a new notification for the user
                    var notification = new Notification
                    {
                        UserId = userId,
                        MilestoneId = milestone.Id,
                        Message = milestone.NotificationMessage,
                        DateCreated = DateTime.Now,
                    };

                    _context.Notifications.Add(notification);
                }
            }

            // saves the changes to the database
            await _context.SaveChangesAsync();
        }

        // Endpoint to fetch all savings for a user
        [HttpGet("get-savings")]
        public async Task<IActionResult> GetSavings()
        {
            var userId = 1;
            var savings = await _context.Savings.Where(s => s.UserId == userId).ToListAsync();

            return Ok(savings);
        }

        // Endpoint for fetching the total savings amount for a user
        [HttpGet("get-user-total-savings")]
        public async Task<IActionResult> GetTotalSavings()
        {
            var userId = 1;
            var totalSavings = await _context.TotalSavings.FirstOrDefaultAsync(ts => ts.UserId == userId);

            var totalAmount = totalSavings?.TotalAmount ?? 0;
            return Ok(totalAmount);

        }

        // Endpoint for fetching savings of friends
        [HttpGet("compare-savings-with-friends")]
        public async Task<IActionResult> CompareSavingsWithFriends()
        {
            var userId = 1; // Hardcoded, should not be

            // Fetch the user's friends
            var friendships = await _context.Friendships
                .Where(f => f.User1Id == userId || f.User2Id == userId)
                .ToListAsync();

            var friendIds = friendships.Select(f => f.User1Id == userId ? f.User2Id : f.User1Id).ToList();

            // Fetch the total savings of the users friends
            var friendsSavings = await _context.TotalSavings
                .Where(ts => friendIds.Contains(ts.UserId))
                .ToListAsync();

            return Ok(friendsSavings);
        }

    }
}
