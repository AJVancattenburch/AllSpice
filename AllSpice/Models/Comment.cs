namespace AllSpice.Models;

public class Comment
{
  public int Id { get; set; }
  public string Title { get; set; }
  public string Photo { get; set; }
  public string Body { get; set; }
  public string Tags { get; set; }
  public string AccountId { get; set; }
  public int RecipeId { get; set; }
  public Account Creator { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}