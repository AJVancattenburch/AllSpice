namespace AllSpice.Services;

public class FavoritesService
{
  private readonly FavoritesRepository _repo;

  public FavoritesService(FavoritesRepository repo)
  {
    _repo = repo;
  }

  // internal void DeleteFavorite(int favoriteId, string userId)
  // {
  //   Favorite fav = _repo.GetFavoriteById(favoriteId);
  //   if (fav.AccountId == userId)
  //   {
  //     throw new Exception("Unauthorized to delete");
  //   }
  //  int rows =  _repo.DeleteFavorite(favoriteId);
  //  if (rows > 1) 
  //  {
  //    throw new Exception("Something has gone terribly wrong");
  //  }
  // }

  internal Favorite CreateFavorite(Favorite favoriteData)
  {
    Favorite favorite = _repo.CreateFavorite(favoriteData);
    return favorite;
  }

  internal List<FavoritesAccount> GetFavoritesByRecipeId(int recipeId)
  {
    return _repo.GetFavoritesByRecipeId(recipeId);
  }

  internal List<FavoriteRecipe> GetMyFavoriteRecipes(string accountId)
  {
    List<FavoriteRecipe> myFavorites = _repo.GetMyFavoriteRecipes(accountId);
    return myFavorites;
  }

  internal Favorite GetFavoriteById(int favoriteId)
  {
    Favorite favorite = _repo.GetFavoriteById(favoriteId);
    return favorite;
  }

  internal void DeleteFavorite(int favoriteId, string userId)
  {
    Favorite favoriteToDelete = _repo.GetFavoriteById(favoriteId);
    if (favoriteToDelete == null)
    {
      throw new Exception("Invalid Favorite Id");
    }
    if (favoriteToDelete.AccountId != userId)
    {
      throw new Exception("Unauthorized: You have not 'favorited' this recipe yet!");
    }
    int rows = _repo.DeleteFavorite(favoriteId);
    if (rows > 1)
    {
      throw new Exception("Something has gone terribly wrong! Multiple favorites have been deleted!");
    }
  }
}