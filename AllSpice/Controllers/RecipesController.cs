namespace AllSpice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
  private readonly RecipesService _recipesService;
  private readonly RecipesRepository _repo;
  private readonly IngredientsService _ingredientsService;
  private readonly FavoritesService _favoritesService;
  private readonly Auth0Provider _auth;

  public RecipesController(RecipesService recipesService, RecipesRepository repo, Auth0Provider auth, IngredientsService ingredientsService, FavoritesService favoritesService)
  {
    _recipesService = recipesService;
    _repo = repo;
    _auth = auth;
    _ingredientsService = ingredientsService;
    _favoritesService = favoritesService;
    

  }
  
  [HttpPost]
  [Authorize]

  public async Task<ActionResult<Recipe>> CreateRecipe([FromBody] Recipe recipeData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      recipeData.CreatorId = userInfo.Id;

      Recipe recipe = _recipesService.CreateRecipe(recipeData);
      return new ActionResult<Recipe>(Ok(recipe));
    }
    catch (Exception e)
    {
      return new ActionResult<Recipe>(BadRequest(e.Message));
    }
  }

  [HttpGet]
  public ActionResult<List<Recipe>> GetAllRecipes(string category, string query)
  {
    try
    {
      if (query == null)
      {
        List<Recipe> recipes = _recipesService.GetAllRecipes();
        return Ok(recipes);
      }
      else
      {
        List<Recipe> recipes = _recipesService.SearchRecipes(category, query);
        return Ok(recipes);
      }
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  // [HttpGet("categories")]
  // public ActionResult<List<string>> GetRecipesByCategory(string category)
  // {
  //   try
  //   {
  //     if (category == null)
  //     {
  //       List<Recipe> recipes = _recipesService.GetAllRecipes();
  //       return Ok(recipes);
  //     }
  //     else
  //     {
  //       List<Recipe> recipes = _recipesService.GetRecipesByCategory(category);
  //       return Ok(recipes);
  //     }
  //   }
  //   catch (Exception e)
  //   {
  //     return BadRequest(e.Message);
  //   }
  // }

  [HttpGet("{recipeId}")]
  public ActionResult<Recipe> GetRecipeById(int recipeId)
  {
    try
    {
      Recipe recipe = _recipesService.GetRecipeById(recipeId);
      if (recipe == null)
      {
        throw new Exception("Invalid Recipe Id");
      }
      recipe.Popularity++;
      _repo.UpdateRecipe(recipe);
      return Ok(recipe);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{recipeId}/ingredients")]
  public ActionResult<List<Ingredient>> GetIngredientsByRecipeId(int recipeId)
  {
    try
    {
      List<Ingredient> ingredients = _ingredientsService.GetIngredientsByRecipeId(recipeId);
      return Ok(ingredients);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  internal Favorite GetFavoriteById(int favoriteId)
  {
    Favorite favorite = _favoritesService.GetFavoriteById(favoriteId);
    return favorite;
  }

  [HttpGet("{recipeId}/favorites")]

  public ActionResult<List<FavoritesAccount>> GetFavoritesByRecipeId(int recipeId)
  {
    try
    {
      List<FavoritesAccount> favorites = _favoritesService.GetFavoritesByRecipeId(recipeId);
      return Ok(favorites);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpDelete("{recipeId}")]
  [Authorize]

  public async Task<ActionResult<Recipe>> ArchiveRecipe(int recipeId)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      Recipe recipe = _recipesService.ArchiveRecipe(recipeId, userInfo.Id);
      return Ok(recipe);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpPut("{recipeId}")]
  [Authorize]

  public async Task<ActionResult<Recipe>> UpdateRecipe(int recipeId, [FromBody] Recipe updateData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      Recipe recipe = _recipesService.UpdateRecipe(updateData, userInfo.Id, recipeId);
      return Ok(recipe);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}