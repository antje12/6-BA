using Backend.Interfaces;
using ClassLibrary;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class UserServiceController : ControllerBase
{
    private readonly ILogger<UserServiceController> _logger;
    private readonly IDataProvider _dataProvider;

    public UserServiceController(ILogger<UserServiceController> logger, IDataProvider dataProvider)
    {
        _logger = logger;
        _dataProvider = dataProvider;
    }

    [HttpGet]
    public IEnumerable<User> Get()
    {
        var users = _dataProvider.GetUsers();
        return users.ToArray();
    }

    [HttpGet("GetByName/{name}")]
    public IActionResult GetByName(string name)
    {
        return Ok();
    }

    [HttpGet("GetById/{Id}")]
    public IActionResult GetById(Guid Id)
    {
        return Ok();
    } 
}
