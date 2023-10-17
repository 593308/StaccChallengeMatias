namespace StaccChallenge.Models
{
    /// <summary>
    /// Represents the total savings of a user.
    /// </summary>
    public class TotalSavings
    {
        public int Id { get; set; } // Unique identifier for the total savings.
        public int UserId { get; set; } // Foreign key for the user.
        public User User { get; set; } // Navigation property for the user.
        public decimal TotalAmount { get; set; } // Total amount saved by the user.
    }
}
