namespace AllSpice.Repositories;

public class CommentsRepository
{
  private readonly IDbConnection _db;

  public CommentsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal void DeleteComment(int commentId)
  {
    string sql = @"
    DELETE FROM
    comments
    WHERE comments.id = @commentId;
    ";

    _db.Execute(sql, new { commentId });
  }

  internal Comment GetCommentById(int commentId)
  {
    string sql = @"
    SELECT
    comments.*,
    accounts.*
    FROM comments
    JOIN accounts ON accounts.id = comments.accountId
    WHERE comments.id = @commentId;
    ";

    Comment comment = _db.Query<Comment, Account, Comment>(sql, (comment, account) =>
    {
      comment.Creator = account;
      return comment;
    }, new { commentId }).FirstOrDefault();

    return comment;
  }

  internal List<Comment> GetCommentsByRecipeId(int recipeId)
  {
    string sql = @"
    SELECT
    comments.*,
    accounts.*
    FROM comments
    JOIN accounts ON accounts.id = comments.accountId
    WHERE comments.recipeId = @recipeId;
    ";

    List<Comment> comments = _db.Query<Comment, Account, Comment>(sql, (comment, account) =>
    {
      comment.Creator = account;
      return comment;
    }, new { recipeId }).ToList();

    return comments;
  }

  internal Comment CreateComment(Comment commentData)
  {
    string sql = @"
    INSERT INTO comments
    (accountId, recipeId, title, photo, body, tags)
    VALUES
    (@accountId, @recipeId, @title, @photo, @body, @tags);

    SELECT
    comments.*,
    accounts.*
    FROM comments
    JOIN accounts ON accounts.id = comments.accountId
    WHERE comments.id = LAST_INSERT_ID();
    ";

    Comment comment = _db.Query<Comment, Account, Comment>(sql, (comment, account) =>
    {
      comment.Creator = account;
      return comment;
    }, commentData).FirstOrDefault();

    return comment;
  }
}