namespace AllSpice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientsController : ControllerBase
{
  private readonly IngredientsService _ingredientsService;
  private readonly Auth0Provider _auth;

  public IngredientsController(IngredientsService ingredientsService, Auth0Provider auth)
  {
    _ingredientsService = ingredientsService;
    _auth = auth;
  }

  [HttpPost]
  [Authorize]

  public async Task<ActionResult<Ingredient>> CreateIngredient([FromBody] Ingredient ingredientData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      ingredientData.CreatorId = userInfo.Id;

      Ingredient ingredient = _ingredientsService.CreateIngredient(ingredientData);
      return new ActionResult<Ingredient>(Ok(ingredient));
    }
    catch (Exception e)
    {
      return new ActionResult<Ingredient>(BadRequest(e.Message));
    }
  }

  [HttpGet]
  public ActionResult<List<Ingredient>> GetAllIngredients()
  {
    try
    {
      List<Ingredient> ingredients = _ingredientsService.GetAllIngredients();
      return Ok(ingredients);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{ingredientId}")]
  public ActionResult<Ingredient> GetIngredientById(int ingredientId)
  {
    try
    {
      Ingredient ingredient = _ingredientsService.GetIngredientById(ingredientId);
      return Ok(ingredient);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  // [HttpDelete("{ingredientId}")]
  // [Authorize]
  // public async Task<ActionResult<Ingredient>> DeleteIngredient(int ingredientId)
  // {
  //   try
  //   {
  //     Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
  //     _ingredientsService.DeleteIngredient(ingredientId, userInfo.Id);
  //     return Ok("Successfully Deleted");
  //   }
  //   catch (Exception e)
  //   {
  //     return BadRequest(e.Message);
  //   }
  // }
}