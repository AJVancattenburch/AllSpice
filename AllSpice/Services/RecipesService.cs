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
    _repo.ArchiveRecipe(recipeId);
    return recipe;
  }

  internal Recipe CreateRecipe(Recipe updateRecipe)
  {
    Recipe recipe = _repo.CreateRecipe(updateRecipe);
    return recipe;
  }

  internal List<Recipe> GetAllRecipes()
  {
  List<Recipe> recipes = _repo.GetAllRecipes();
    return recipes;
  }

  // public List<Recipe> GetRecipesByCategory(string category)
  // {
  //   return _repo.GetRecipesByCategory(category);
  // }


  internal Recipe GetRecipeById(int recipeId)
  {
    Recipe recipe = _repo.GetRecipeById(recipeId);
    if (recipe == null)
    {
      throw new Exception("Invalid Recipe Id");
    }
    return recipe;
  }

  internal Recipe UpdateRecipe(Recipe updateRecipe, string userId, int recipeId)
  {
    Recipe original = GetRecipeById(recipeId);
    if (original.CreatorId != userId)
    {
      throw new Exception("You cannot edit another users recipe");
    }

    original.Title = updateRecipe.Title != null ? updateRecipe.Title : original.Title;
    original.Instructions = updateRecipe.Instructions != null ? updateRecipe.Instructions : original.Instructions;
    original.Img = updateRecipe.Img != null ? updateRecipe.Img : original.Img;
    original.Category = updateRecipe.Category != null ? updateRecipe.Category : original.Category;
    original.Tags = updateRecipe.Tags != null ? updateRecipe.Tags : original.Tags;
    original.Popularity = updateRecipe.Popularity == 0 ? original.Popularity : updateRecipe.Popularity;
    _repo.UpdateRecipe(original);
    return original;
  }

  internal List<Recipe> SearchRecipes(string category, string query)
  {
    List<Recipe> recipes = _repo.SearchRecipes(category, query);
    return recipes;
  }
}
