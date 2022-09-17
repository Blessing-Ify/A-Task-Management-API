namespace NewAPI.Model
{
    public class UserTask
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string TaskId { get; set; }
        public User User { get; set; }
        public Task Task { get; set; }
    }
}