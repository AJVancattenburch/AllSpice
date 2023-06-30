namespace AllSpice.Services;

// public class FavoritesService
// {
//   private readonly FavoritesRepository _repo;

//   public FavoritesService(FavoritesRepository repo)
//   {
//     _repo = repo;
//   }

//   internal Favorite CreateFavorite(Favorite favoriteData)
//   {
//     Favorite favorite = _repo.CreateFavorite(favoriteData);
//     return favorite;
//   }

//   internal List<Favorite> GetAllFavorites()
//   {
//     List<Favorite> favorites = _repo.GetAllFavorites();
//     return favorites;
//   }

//   internal Favorite GetFavoriteById(int favoriteId)
//   {
//     return _repo.GetFavoriteById(favoriteId);
//   }

//   internal List<Favorite> GetFavoritesByAccountId(string accountId)
//   {
//     return _repo.GetFavoritesByAccountId(accountId);
//   }

//   internal void DeleteFavorite(int favoriteId, string accountId)
//   {
//     Favorite favoriteToDelete = GetFavoriteById(favoriteId);
//     if (favoriteToDelete == null)
//     {
//       throw new Exception("Invalid Favorite Id");
//     }
//     if (favoriteToDelete.AccountId != accountId)
//     {
//       throw new Exception("This is not your favorite");
//     }
//     _repo.DeleteFavorite(favoriteId);
//   }

//   internal List<Recipe> GetRecipesByFavoriteId(int favoriteId)
//   {
//     return _repo.GetRecipesByFavoriteId(favoriteId);
//   }
// }