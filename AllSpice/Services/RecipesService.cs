namespace AllSpice.Services;

public class RecipesService
{
  private readonly RecipesRepository _repo;

  public RecipesService(RecipesRepository repo)
  {
    _repo = repo;
  }

  internal Recipe ArchiveRecipe(int recipeId, string userId)
  {
    Recipe recipe = GetRecipeById(recipeId);
    if (recipe.CreatorId != userId)
    {
      throw new Exception("You cannot delete another users recipe");
    }
    recipe.Archived = !recipe.Archived;
    _repo.UpdateRecipe(recipe);
    return recipe;
  }

  internal Recipe CreateRecipe(Recipe recipeData)
  {
    Recipe recipe = _repo.CreateRecipe(recipeData);
    return recipe;
  }

  internal List<Recipe> GetAllRecipes()
  {
  List<Recipe> recipes = _repo.GetAllRecipes();
    return recipes;
  }

  internal Recipe GetRecipeById(int recipeId)
  {
    Recipe recipe = _repo.GetRecipeById(recipeId);
    if (recipe == null)
    {
      throw new Exception("Invalid Recipe Id");
    }
    return recipe;
  }
}