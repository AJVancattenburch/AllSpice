namespace AllSpice.Models;

public class Account
{
  public string Id { get; set; }
  public string Name { get; set; }
  public string Email { get; set; }
  public string Picture { get; set; }
}

// NOTE - THIS CREATES A MANY TO MANY RELATIONSHIP BETWEEN FAVORITES AND THE SPECIFIC USER ACCOUNT HOLDER
public class FavoritesAccount : Account
{
  public int FavoriteId { get; set; }
}