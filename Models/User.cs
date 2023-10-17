using Microsoft.AspNetCore.Identity;

namespace StaccChallenge.Models
{
    /// <summary>
    /// Represents the different roles a user can have in the system.
    /// </summary>
    public enum UserRole
    {
        User,   // Represents a standard user.
        Admin,  // Represents an administrator with elevated privileges.
    }

    /// <summary>
    /// Represents a user in the system.
    /// </summary>
    public class User
    {
        public int Id { get; set; } // Unique identifier for the user.
        public required string Username { get; set; } // Username of the user.
        public required string Email { get; set; } // Email address of the user.
        public byte[]? PasswordHash { get; set; } // Hashed password for the user.
        public byte[]? PasswordSalt { get; set; } // Salt used for hashing the password.
        public UserRole Role { get; set; } // Role of the user (e.g., User, Admin).

        // Navigation properties for related entities.
        public ICollection<Saving> Savings { get; set; }
        public ICollection<FriendRequest> SentRequests { get; set; }
        public ICollection<FriendRequest> ReceivedRequests { get; set; }
        public ICollection<Friendship> Friendships { get; set; }
        public ICollection<Notification> Notifications { get; set; }
    }
}
