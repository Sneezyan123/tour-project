namespace TourProject.Models
{
    public class Tour
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; }
        public User Creator { get; set; }
        public Guid CreatorId { get; set; }
        public List<UserJoinsTour> JoinedUsers { get; set; } = [];
    }
}
