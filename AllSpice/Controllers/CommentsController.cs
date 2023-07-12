namespace AllSpice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
  private readonly CommentsService _commentsService;
  private readonly Auth0Provider _auth;
  public CommentsController(Auth0Provider auth, CommentsService commentsService)
  {
    _auth = auth;
    _commentsService = commentsService;
  }

  [HttpDelete("{commentId}")]
  [Authorize]
  public async Task<ActionResult<string>> DeleteComment(int commentId)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      string message = _commentsService.DeleteComment(userInfo.Id, commentId);
      return Ok(message);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{commentId}")]
  public ActionResult<Comment> GetCommentById(int commentId)
  {
    try
    {
      Comment comment = _commentsService.GetCommentById(commentId);
      return Ok(comment);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Comment>> CreateComment([FromBody] Comment commentData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      commentData.AccountId = userInfo.Id;
      Comment comment = _commentsService.CreateComment(commentData);
      return Ok(comment);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}