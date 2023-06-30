namespace AllSpice.Models;

public class Favorite
{
  public int Id { get; set; }
  public string AccountId { get; set; }
  public int RecipeId { get; set; }
}

// NOTE - THIS CREATES A MANY TO MANY RELATIONSHIP BETWEEN FAVORITES AND RECIPES
public class FavoriteRecipe : Recipe
{
  public int FavoriteId { get; set; }
}