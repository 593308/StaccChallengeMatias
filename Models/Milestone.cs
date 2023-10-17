namespace StaccChallenge.Models
{
    /// <summary>
    /// Represents a milestone that users can achieve.
    /// </summary>
    public class Milestone
    {
        public int Id { get; set; } // Unique identifier for the milestone.
        public decimal Amount { get; set; } // Amount required to achieve this milestone.
        public string Title { get; set; } // Name or title of the milestone.
        public string BadgeUrl { get; set; } // URL for the badge or image representing the milestone.
        public DateTime DateAchieved { get; set; } // Date when the milestone was achieved.

        // Additional details about the milestone.
        public string Description { get; set; }
        public string Reward { get; set; }
        public string NotificationMessage { get; set; }
    }
}
