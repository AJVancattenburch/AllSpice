namespace AllSpice.Repositories;

public class FavoritesRepository
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
      (accountId, recipeId)
    VALUES
      (@accountId, @recipeId)
    SELECT
    LAST_INSERT_ID()
    ;";

    int lastInsertId = _db.ExecuteScalar<int>(sql, favoriteData);
    favoriteData.Id = lastInsertId;
    return favoriteData;
  }

  internal List<Favorite> GetAllFavorites()
  {
    string sql = @"
    SELECT
      fav.*,
      acc.*
    FROM favorites fav
    JOIN accounts acc 
      ON fav.accountId = acc.id;
    WHERE account acc
      ON acc.id = fav.accountId
    ;";
    List<Favorite> favorites = _db.Query<Favorite, Account, Favorite>(sql, (favorite, account) =>
    {
      favorite.Creator = account;
      return favorite;
    }).ToList();
  //   WHERE id = @favoriteId;
      return favorites;
    }
  }
// }

  // internal Favorite GetFavoriteById(int favoriteId)
  // {
  //   string sql = @"
  //   SELECT
  //     fav.*,
  //     acc.*
  //   FROM favorites fav
  //   JOIN accounts acc ON fav.accountId = acc.id
  //   WHERE fav.id = @favoriteId;
  //   ";
  //   return _db.Query<Favorite, Account, Favorite>(sql, (favorite, account) =>
  //   {
  //     favorite.Creator = account;
  //     return favorite;
  //   }, new { favoriteId }).FirstOrDefault();
  // }

  // internal List<Favorite> GetFavoritesByAccountId(string accountId)
  // {
  //   string sql = @"
  //   SELECT
  //     fav.*,
  //     acc.*
  //   FROM favorites fav
  //   JOIN accounts acc ON fav.accountId = acc.id
  //   WHERE fav.accountId = @accountId;
  //   ";
  //   return _db.Query<Favorite, Account, Favorite>(sql, (favorite, account) =>
  //   {
  //     favorite.Creator = account;
  //     return favorite;
  //   }, new { accountId }).ToList();
  // }

  // internal Favorite DeleteFavorite(int favoriteId)
  // {
  //   string sql = @"
  //   DELETE FROM favorites
  //   ";
  //   _db.Execute(sql, new { favoriteId });
  //     return null;
  // }