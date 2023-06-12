using System.ComponentModel.DataAnnotations;
namespace CommentService.Models;

public class Comment
{
    public string userID { get; set; }
    public string postID { get; set; }
    public string time { get; set; }
    public string message { get; set; }
    public string image { get; set; }
    [Key] public string id { get; set; }

    public Comment(string time, string message, string id, string postID)
    {
        this.time = time;
        this.message = message;
        this.id = id;
        this.postID = postID;
    }

    public Comment()
    {
        
    }

    public Comment(string id)
    {
        this.id = id;
    }
}