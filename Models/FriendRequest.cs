namespace StaccChallenge.Models
{
    /// <summary>
    /// Friend request between two users.
    /// </summary>
    public class FriendRequest
    {

        public int Id { get; set; }
        public int RequesterId { get; set; }
        public User Requester { get; set; }

        public int RequestedId { get; set; }
        public User Requested { get; set; }

        public DateTime RequestDate { get; set; }
    }
}
