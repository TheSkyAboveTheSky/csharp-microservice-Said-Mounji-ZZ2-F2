namespace FrontAppBlazor.Entities
{
  public class Project

  {
    public Project(string nom, string description, int groupId = 0,string status = "UpComing")
    {
      Id = GenerateProjectId();
      Nom = nom;
      Description = description;
      Status = status;
      GroupId = groupId;
    }
    private string GenerateProjectId()
    {
      return "project-" + Guid.NewGuid().ToString().Substring(0, 6);
    }
    public string Id { get; set; }
    public string Nom { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public int GroupId { get; set; }
  }
  public class ProjectModelUpdate
  {
    public string? Nom { get; set; }
    public string? Description { get; set; }
    public string? Status { get; set; }
    public int? GroupId { get; set; }
  }
}