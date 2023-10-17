
namespace StaccChallenge.Models
{
    /// <summary>
    /// Represents a notification sent to a user.
    /// </summary>
    public class Notification
    {
        public int Id { get; set; } // Unique identifier for the notification.
        public int UserId { get; set; } // Foreign key for the user receiving the notification.
        public User? User { get; set; } // Navigation property for the user.
        public string? Content { get; set; } // Content or body of the notification.
        public DateTime DateCreated { get; set; } // Date when the notification was created.
        public bool IsRead { get; set; } // Indicates if the notification has been read.
        public NotificationType Type { get; set; } // Type of the notification (e.g., FriendRequest, MilestoneReached).
        public string? Message { get; set; } // Message associated with the notification.

        // Foreign key and navigation property for the milestone (if the notification is about a milestone).
        public int? MilestoneId { get; set; }
        public Milestone? Milestone { get; set; }

        // Enum representing the different types of notifications.
        public enum NotificationType
        {
            FriendRequest,
            MilestoneReached
        }
    }
}
