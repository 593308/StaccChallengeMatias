namespace StaccChallenge.Models
{
    /// <summary>
    /// Represents a saving made by a user.
    /// </summary>
    public class Saving
    {
        public int Id { get; set; } // Unique identifier for the saving.
        public int UserId { get; set; } // Foreign key for the user who made the saving.
        public User User { get; set; } // Navigation property for the user.
        public decimal Amount { get; set; } // Amount saved.
        public DateTime DateSaved { get; set; } // Date when the saving was made.
    }
}
