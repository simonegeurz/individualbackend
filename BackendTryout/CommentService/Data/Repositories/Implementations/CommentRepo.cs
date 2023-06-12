using CommentService.Data.Repositories.Interfaces;
using CommentService.Models;
namespace CommentService.Data.Repositories.Implementations;

public class CommentRepo : ICommentRepo
{
    private readonly AppDbContext _dbContext;

    public CommentRepo(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool SaveChanges()
    {
        return (_dbContext.SaveChanges() >= 0);
    }

    public void AddToCommentDb(Comment newComment)
    {
        if (_dbContext.newCommentsSimone == null) return;
        _dbContext.newCommentsSimone.Add(newComment);
        _dbContext.SaveChanges();
    }

    public List<Comment> GetUserComment()
    {
        if (_dbContext.newCommentsSimone != null)
        {
           return _dbContext.newCommentsSimone.ToList();
        }
        return new List<Comment>();
    }

    public List<Comment> GetCommentsOfUser(string user)
    {
        if (_dbContext.newCommentsSimone != null)
        {
            return _dbContext.newCommentsSimone.Where(comment => comment.userID == user).ToList();
        }

        return new List<Comment>();
    }
    public List<Comment> GetCommentsOfPost(string postid)
    {
        if (_dbContext.newCommentsSimone != null)
        {
            return _dbContext.newCommentsSimone.Where(comment => comment.postID == postid).ToList();
        }

        return new List<Comment>();
    }

    public Comment GetSinglePost(string commentId)
    {
        if (_dbContext.newCommentsSimone != null)
        {
            return _dbContext.newCommentsSimone.FirstOrDefault(x => x.id == commentId);
        }

        return new Comment();
    }

    public void UpdateComment(string id, Comment comment)
    {
        if (_dbContext.newCommentsSimone != null)
        {
            _dbContext.Remove(_dbContext.newCommentsSimone.Single(a => a.id == id));
            _dbContext.SaveChanges();

            _dbContext.Add(comment);
            _dbContext.SaveChanges();
        }
    }

    public void DeleteComment(string user)
    {
        if (_dbContext.newCommentsSimone != null)
        {
            foreach (Comment comment in _dbContext.newCommentsSimone.Where(a => a.userID == user))
            {
                _dbContext.Remove(_dbContext.newCommentsSimone.FirstOrDefault(x => x.userID == user));
                _dbContext.SaveChanges();

            }

        }
    }




}