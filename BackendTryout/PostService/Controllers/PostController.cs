using Microsoft.AspNetCore.Mvc;
using PostService.Models;
using PostService.Data.Repositories.Interfaces;
namespace PostService.Controllers;

using Microsoft.Extensions.Options;
using PostService.Data.Repositories;
using RabbitMQ.Client;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class PostController : ControllerBase
{

    private readonly IPostRepo _postRepo;

    private readonly IOptions<RabbitMq> config;


    public PostController(IPostRepo postRepo, IOptions<RabbitMq> config)
    {
        _postRepo = postRepo;
        this.config = config;

    }

    [HttpPost]
    [Route("place/{userId}")]
    public void AddPost([FromBody] Post newPost)
    {
        _postRepo.AddToPostDb(newPost);
        IConnectionFactory connectionFactory = new ConnectionFactory
        {
            HostName = config.Value.hostname,
            Port = config.Value.port,
            UserName = config.Value.username,
            Password = config.Value.password
        };
        using (IConnection connection = connectionFactory.CreateConnection())
        {
            IModel channel = connection.CreateModel();
            channel.ExchangeDeclare("Exchange", ExchangeType.Topic, true);
            string test = "First try rabbitmq";
            byte[] body = Encoding.Unicode.GetBytes(test);
            channel.BasicPublish("Exchange", "FirstTry", null, body);
        }
        Console.WriteLine("I published");
    }

    [HttpGet]
    [Route("{user}")]
    public IActionResult GetUserPosts()
    {
        return Ok(_postRepo.GetUserPost());
    }

    [HttpGet]
    [Route("getpost/{user}")]
    public IActionResult GetPostOfUser(string user)
    {
        return Ok(_postRepo.GetPostOfUser(user));
    }

    [HttpDelete]
    [Route("deletepost/{user}")]
    public void DeletePostOfUSer(string user)
    {
        _postRepo.DeletePosts(user);
    }
    
   

}
