namespace AllSpice.Repositories;

public class NewBaseType
{
  internal Favorite GetFavoriteByRecipeId(int recipeId)
  {
    throw new NotImplementedException();
  }
}

public class FavoritesRepository : NewBaseType
{
  private readonly IDbConnection _db;

  public FavoritesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Favorite CreateFavorite(Favorite favoriteData)
  {
    string sql = @"
    INSERT INTO favorites
    (recipeId, accountId)
    VALUES
    (@RecipeId, @AccountId);
    SELECT LAST_INSERT_ID()
    ;";

    int lastInsertId = _db.ExecuteScalar<int>(sql, favoriteData);
    favoriteData.Id = lastInsertId;
    return favoriteData;
  }

  internal Favorite GetFavoriteById(int favoriteId)
  {
    string sql = @"
    SELECT
      favs.*
    FROM favorites favs
    JOIN accounts acc
      ON acc.id = favs.accountId
    WHERE favs.id = @favoriteId
    ;";

    Favorite favs = _db.Query<Favorite>(sql, new { favoriteId }).FirstOrDefault();
    return favs;
  }

  internal int DeleteFavorite(int favoriteId)
  {
    string sql = @"
    DELETE
    FROM favorites 
    WHERE id = @favoriteId 
    LIMIT 1
    ;";

    int rows = _db.Execute(sql, new { favoriteId });
    return rows;
  }

  internal List<FavoritesAccount> GetFavoritesByRecipeId(int recipeId)
  {
    string sql = @"
    SELECT
      favs.*,
      acc.*
    FROM favorites favs
    JOIN accounts acc 
      ON acc.id = favs.accountId
    WHERE favs.recipeId = @recipeId
    ;";
    
    List<FavoritesAccount> favorite = _db.Query<FavoritesAccount, Account, FavoritesAccount>(sql, (favorite, account) =>
    {
      favorite.Id = account.Id;
      return favorite;
    }, new { recipeId }).ToList();
    return favorite;
}

  internal List<FavoriteRecipe> GetMyFavoriteRecipes(string accountId)
  {
    string sql = @"
    SELECT
      favs.*,
      rec.*,
      acc.*
    FROM favorites favs
    JOIN recipes rec 
      ON rec.id = favs.recipeId
    JOIN accounts acc
      ON acc.id = rec.creatorId
    WHERE favs.accountId = @accountId
    ORDER BY rec.createdAt DESC
    ;";

    List<FavoriteRecipe> myFavorites = _db.Query<Favorite, FavoriteRecipe, Account, FavoriteRecipe>(sql, (favorites, recipe, account) =>
    {
      recipe.FavoriteId = favorites.Id;
      recipe.Creator = account;
      return recipe;
    }, new { accountId }).ToList();
      return myFavorites;
  }
}
