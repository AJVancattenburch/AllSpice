namespace AllSpice.Models;

public class Recipe
{
  public int Id { get; set; }
  public string Title { get; set; }
  public string Instructions { get; set; }
  public string img { get; set; }
  public string category { get; set; }
  public bool Archived { get; set; }
  public string CreatorId { get; set; }
  public Account Creator { get; set; }
}

public class FavoritesRecipe : Recipe
{
  public int FavoriteId { get; set; }
}