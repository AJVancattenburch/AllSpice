namespace AllSpice.Models;

public class Profile
{
  public string Id { get; set; }
  public string Name { get; set; }
  public string Picture { get; set; }
}

// NOTE - THIS GIVES THE USER ACCOUNT ACCESS TO THE PROFILE CLASS WHILE LIMITING PRIVATE INFORMATION
public class Account : Profile
{
  public string Email { get; set; }
}

// NOTE - THIS CREATES A MANY TO MANY RELATIONSHIP BETWEEN FAVORITES AND THE SPECIFIC USER ACCOUNT HOLDER
public class FavoritesAccount : Profile
{
  public int FavoriteId { get; set; }
}
