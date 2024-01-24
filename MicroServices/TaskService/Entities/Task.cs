namespace TaskService.Entities
{
    public class Task
    {
        public Task(string titre, string description, string userId)
        {
            Titre = titre;
            Description = description;
            UserId = userId;
        }
        public string Id { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
    }
}