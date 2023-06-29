namespace AllSpice.Services;

public class IngredientsService
{
  private readonly IngredientsRepository _repo;
  
  public IngredientsService(IngredientsRepository repo)
  {
    _repo = repo;
  }

  internal List<Ingredient> GetAllIngredients()
  {
    return _repo.GetAllIngredients();
  }

  internal Ingredient CreateIngredient(Ingredient ingredientData)
  {
    return _repo.CreateIngredient(ingredientData);
  }

  internal Ingredient GetIngredientById(int ingredientId)
  {
    Ingredient ingredient = _repo.GetIngredientById(ingredientId);
    if (ingredient == null)
    {
      throw new Exception("Invalid ingredient Id");
    }
    return ingredient;
  }

  // internal Ingredient DeleteIngredient(int ingredientId, string recipeId)
  // {
  //   Ingredient ingredient = _repo.GetIngredientById(ingredientId);
  //   if (ingredient.CreatorId != recipeId)
  //   {
  //     throw new Exception("You cannot delete another users recipe ingredients");
  //   }
  //   _repo.DeleteIngredient(ingredientId);
  // }
}