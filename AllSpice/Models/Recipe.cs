namespace AllSpice.Models;

public class Recipe
{
  public int Id { get; set; }
  public string Title { get; set; }
  public string Instructions { get; set; }
  public string Img { get; set; }
  public string Category { get; set; }
  public string Tags { get; set; }
  public int Popularity { get; set; }
  public bool Archived { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public Profile Creator { get; set; }
  public string CreatorId { get; set; }
}

public class FavoriteRecipe : Recipe
{
  public int FavoriteId { get; set; }
}