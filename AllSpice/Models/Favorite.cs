namespace AllSpice.Models;

public class Favorite
{
  public int Id { get; set; }
  public string AccountId { get; set; }
  public int RecipeId { get; set; }
  public Account Creator { get; set; }
}

// NOTE - THIS CREATES A MANY TO MANY RELATIONSHIP BETWEEN FAVORITES AND RECIPES
public class FavoriteRecipe : Recipe
{
  public int FavoriteId { get; set; }
}

// NOTE - THIS CREATES A MANY TO MANY RELATIONSHIP BETWEEN FAVORITES AND THE SPECIFIC USER ACCOUNT HOLDER
public class FavoriteAccount : Account
{
  public int FavoriteId { get; set; }
}