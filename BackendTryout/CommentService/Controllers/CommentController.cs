using Microsoft.AspNetCore.Mvc;
using CommentService.Models;
using CommentService.Data.Repositories.Interfaces;
namespace CommentService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController : ControllerBase
{

    private readonly ICommentRepo _commentRepo;

    public CommentController(ICommentRepo commentRepo)
    {
        _commentRepo = commentRepo;

    }

    [HttpPost]
    [Route("{userId}")]
    public void AddComment([FromBody] Comment newComment)
    {
        _commentRepo.AddToCommentDb(newComment);
    }

    [HttpGet]
    [Route("{user}")]
    public IActionResult GetUserComments()
    {
        return Ok(_commentRepo.GetUserComment());
    }

    [HttpGet]
    [Route("getcomment/{user}")]
    public IActionResult GetCommentOfUser(string user)
    {
        return Ok(_commentRepo.GetCommentsOfUser(user));
    }

    [HttpGet]
    [Route("getcomment/post/{postid}")]
    public IActionResult GetCommentOfPost(string postid)
    {
        return Ok(_commentRepo.GetCommentsOfPost(postid));
    }

    [HttpDelete]
    [Route("deletecomment/{user}")]
    public void DeleteCommentOfUSer(string user)
    {
        _commentRepo.DeleteComment(user);
    }




}
