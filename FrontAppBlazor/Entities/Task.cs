namespace FrontAppBlazor.Entities
{
  public class Task
  {
    public Task(string titre, string description, string userId)
    {
      Id = GenerateUserId();
      Titre = titre;
      Description = description;
      UserId = userId;
    }
    private string GenerateUserId()
    {
      return "task-" + Guid.NewGuid().ToString().Substring(0, 6);
    }
    public string Id { get; set; }
    public string Titre { get; set; }
    public string Description { get; set; }
    public string UserId { get; set; }
  }
}