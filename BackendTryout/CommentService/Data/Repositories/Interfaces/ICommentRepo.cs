using CommentService.Models;
namespace CommentService.Data.Repositories.Interfaces;

public interface ICommentRepo
{
    bool SaveChanges();
    void AddToCommentDb(Comment newComment);

    List<Comment> GetUserComment();

    List<Comment> GetCommentsOfUser(string user);

    List<Comment> GetCommentsOfPost(string postID);

    void DeleteComment(string user);            

    
}