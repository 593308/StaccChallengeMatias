using static StaccChallenge.Models.Notification;

namespace StaccChallenge.Dtos
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsRead { get; set; }
        public NotificationType Type { get; set; }
    }
}
