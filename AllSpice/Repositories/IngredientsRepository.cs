namespace AllSpice.Repositories;

public class IngredientsRepository
{
  private readonly IDbConnection _db;

  public IngredientsRepository(IDbConnection db)
  {
    _db = db;
  }

  // internal List<Ingredient> GetAllIngredients()
  // {
  //   string sql = @"
  //   SELECT
  //     ing.*,
  //     acc.*
  //   FROM ingredients ing
  //   JOIN accounts acc ON ing.creatorId = acc.id;
  //   ";
  //   List<Ingredient> ingredients = _db.Query<Ingredient, Account, Ingredient>(sql, (ingredient, account) =>
  //   {
  //     ingredient.Creator = account;
  //     return ingredient;
  //   }).ToList();
  //     return ingredients;
  // }

  internal Ingredient CreateIngredient(Ingredient ingredientData)
  {
    string sql = @"
    INSERT INTO ingredients
      (name, quantity, recipeId)
    VALUES
      (@name, @quantity, @recipeId);
    SELECT
      LAST_INSERT_ID()
    ;";

    int id = _db.ExecuteScalar<int>(sql, ingredientData);
    ingredientData.Id = id;
    return ingredientData;
  }

  internal int DeleteIngredient(int ingredientId)
  {
    string sql = @"
    DELETE FROM ingredients 
    WHERE id = @ingredientId 
    LIMIT 1;
    ;";
    int rows = _db.Execute(sql, new { ingredientId });
    return rows;
  }

  internal Ingredient GetIngredientById(int ingredientId)
  {
    string sql = @"
    SELECT
    *
    FROM ingredients
    WHERE id = @ingredientId
    ;";

    Ingredient ingredient = _db.Query<Ingredient>(sql, new { ingredientId }).FirstOrDefault();
    return ingredient;
    
  }

  internal List<Ingredient> GetIngredientsByRecipeId(int recipeId)
  {
    string sql = @"
    SELECT
      ing.*,
      rec.*
    FROM ingredients ing
    JOIN recipes rec
      ON rec.id = ing.recipeId
    WHERE ing.recipeId = @recipeId
    ;";
    List<Ingredient> ingredients = _db.Query<Ingredient, Account, Ingredient>(sql, (ingredient, recipe) =>
    {
      ingredient.RecipeId = recipeId;
      return ingredient;
    }, new { recipeId }).ToList();
    return ingredients;
  }
}