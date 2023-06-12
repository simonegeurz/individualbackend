using PostService.Models;
namespace PostService.Data.Repositories.Interfaces;

public interface IPostRepo
{
    bool SaveChanges();
    void AddToPostDb(Post newPost);

    List<Post> GetUserPost();

    List<Post> GetPostOfUser(string user);

    void DeletePosts(string user);

    
}