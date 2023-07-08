namespace AllSpice.Models;

public class Comment
{
  public int Id { get; set; }
  public string Body { get; set; }
  public string AccountId { get; set; }
  public int RecipeId { get; set; }
  public Account Creator { get; set; }
}