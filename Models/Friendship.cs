namespace StaccChallenge.Models
{
    /// <summary>
    /// Represents a friendship relation between two users.
    /// </summary>
    public class Friendship
    {
        public int Id { get; set; } // Unique identifier for the friendship.

        // Foreign key and navigation property for the first user in the friendship.
        public int User1Id { get; set; }
        public User User1 { get; set; }

        // Foreign key and navigation property for the second user in the friendship.
        public int User2Id { get; set; }
        public User User2 { get; set; }
    }
}
