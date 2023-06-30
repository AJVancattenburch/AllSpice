namespace AllSpice.Controllers;

// [ApiController]
// [Route("api/[controller]")]

// public class FavoritesController : ControllerBase
// {
//   private readonly FavoritesService _favoritesService;
//   private readonly Auth0Provider _auth0;

//   public FavoritesController(FavoritesService favoritesService, Auth0Provider auth0)
//   {
//     _favoritesService = favoritesService;
//     _auth0 = auth0;
//   }

//   [HttpPost]
//   [Authorize]

//   public async Task<ActionResult<Favorite>> CreateFavorite([FromBody] Favorite favoriteData)
//   {
//     try
//     {
//       Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
//       favoriteData.AccountId = userInfo.Id;

//       Favorite favorite = _favoritesService.CreateFavorite(favoriteData);
//       return Ok(favorite);
//     }
//     catch (Exception e)
//     {
//       return BadRequest(e.Message);
//     }
//   }

//   [HttpGet]
//   public ActionResult<List<Favorite>> GetAllFavorites()
//   {
//     try
//     {
//       List<Favorite> favorites = _favoritesService.GetAllFavorites();
//       return Ok(favorites);
//     }
//     catch (Exception e)
//     {
//       return BadRequest(e.Message);
//     }
//   }

//   [HttpDelete("{favoriteId}")]
//   [Authorize]
//   public async Task<ActionResult<Favorite>> DeleteFavorite(int favoriteId)
//   {
//     try
//     {
//       Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
//       _favoritesService.DeleteFavorite(favoriteId, userInfo.Id);
//       return Ok("Successfully Deleted");
//     }
//     catch (Exception e)
//     {
//       return BadRequest(e.Message);
//     }
//   }
// }