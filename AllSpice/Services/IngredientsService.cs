namespace AllSpice.Services;

public class IngredientsService
{
  private readonly IngredientsRepository _repo;

  private readonly RecipesService _recipesService;
  
  public IngredientsService(IngredientsRepository repo, RecipesService recipesService)
  {
    _repo = repo;

    _recipesService = recipesService;
  }

  internal Ingredient CreateIngredient(Ingredient ingredientData)
  {
    Ingredient ingredient = _repo.CreateIngredient(ingredientData);
    return ingredient;

  }

  internal Ingredient GetIngredientById (int ingredientId)
  {
    Ingredient ingredient = _repo.GetIngredientById(ingredientId);
    if (ingredient == null) new Exception ($"Ingredient ID: {ingredientId} is an invalid ID.");
    return ingredient;
  }

  internal void DeleteIngredient(int ingredientId, string userId)
  {
    Ingredient ingredient = GetIngredientById(ingredientId);
    Recipe recipe = _recipesService.GetRecipeById(ingredient.RecipeId);
    if (recipe.CreatorId != userId) new Exception("This is not your ingredient to delete!");
    int rows = _repo.DeleteIngredient(ingredientId);
    if (rows > 1) throw new Exception("Something has gone terribly wrong. More than one row affected");
  }

  internal List<Ingredient> GetIngredientsByRecipeId(int recipeId)
  {
    List<Ingredient> ingredients = _repo.GetIngredientsByRecipeId(recipeId);
    return ingredients;
  }
}