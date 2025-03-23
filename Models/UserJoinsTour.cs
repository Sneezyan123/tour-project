namespace TourProject.Models
{
    public class UserJoinsTour
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid TourId { get; set; }
        public Tour Tour { get; set; }
    }
}
