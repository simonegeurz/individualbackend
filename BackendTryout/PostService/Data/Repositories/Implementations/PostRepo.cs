using PostService.Data.Repositories.Interfaces;
using PostService.Models;
namespace PostService.Data.Repositories.Implementations;

public class PostRepo : IPostRepo
{
    private readonly AppDbContext _dbContext;

    public PostRepo(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool SaveChanges()
    {
        return (_dbContext.SaveChanges() >= 0);
    }

    public void AddToPostDb(Post newPost)
    {
        if (_dbContext.newpostsSimone == null) return;
        _dbContext.newpostsSimone.Add(newPost);
        _dbContext.SaveChanges();
    }

    public List<Post> GetUserPost()
    {
        if (_dbContext.newpostsSimone != null)
        {
           return _dbContext.newpostsSimone.ToList();
        }
        return new List<Post>();
    }

    public List<Post> GetPostOfUser(string user)
    {
        if (_dbContext.newpostsSimone != null)
        {
            return _dbContext.newpostsSimone.Where(post => post.userID == user).ToList();
        }

        return new List<Post>();
    }

    public Post GetSinglePost(string postId)
    {
        if (_dbContext.newpostsSimone != null)
        {
            return _dbContext.newpostsSimone.FirstOrDefault(x => x.id == postId);
        }

        return new Post();
    }

    public void UpdateComment(string id, Post post)
    {
        if (_dbContext.newpostsSimone != null)
        {
            _dbContext.Remove(_dbContext.newpostsSimone.Single(a => a.id == id));
            _dbContext.SaveChanges();

            _dbContext.Add(post);
            _dbContext.SaveChanges();
        }
    }

    public void DeletePosts(string user)
    {
        if (_dbContext.newpostsSimone != null)
        {
            foreach( Post post in _dbContext.newpostsSimone.Where(a => a.userID == user)) {
                _dbContext.Remove(_dbContext.newpostsSimone.FirstOrDefault(x => x.userID == user));
                _dbContext.SaveChanges();

            }

        }
    }




}