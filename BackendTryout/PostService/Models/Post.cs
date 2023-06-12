using System.ComponentModel.DataAnnotations;
namespace PostService.Models;

public class Post
{
    public string userID { get; set; }
    public string time { get; set; }
    public string message { get; set; }
    public string image { get; set; }
    public string commentCount { get; set; }
    [Key] public string id { get; set; }

    public Post(string time, string message, string id)
    {
        this.time = time;
        this.message = message;
        this.id = id;
    }

    public Post()
    {
        
    }

    public Post(string id)
    {
        this.id = id;
    }
}