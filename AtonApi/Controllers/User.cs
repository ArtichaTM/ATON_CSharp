using Microsoft.AspNetCore.Mvc;
using AtonApi.Models;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace AtonApi.Controllers;

[ApiController]
[Route("api/User")]
public class UserController : ControllerBase
{
    UserContext db = new(
        new DbContextOptionsBuilder<UserContext>()
            .UseInMemoryDatabase("MemoryDB")
            .Options
    );
    static public Guid token = Guid.NewGuid();

    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Gets specific user
    /// </summary>
    /// <param name="token">SuperAdmin token</param>
    /// <param name="guid">Target user GUID</param>
    /// <returns>User if found, else null</returns>
    [HttpGet]
    [ProducesResponseType<string>(StatusCodes.Status403Forbidden)]
    [ProducesResponseType<User>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Get(
        [FromQuery] Guid token,
        [FromQuery] Guid guid
    ) {
        if (token != UserController.token) {
            return Forbid("Wrong token");
        }
        User? user = db.Users.Find(guid);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    /// <summary>
    /// Creates new user
    /// </summary>
    /// Login must be unique
    /// <param name="token">SuperAdmin token</param>
    /// <param name="guid">Target user GUID</param>
    /// <returns>User if found, else null</returns>
    [HttpPost]
    [ProducesResponseType<string>(StatusCodes.Status403Forbidden)]
    [ProducesResponseType<Guid>(StatusCodes.Status200OK)]
    [Consumes("application/json")]
    public IActionResult Post(
        [FromQuery] Guid token,
        [FromBody] User user
    ) {
        if (token != UserController.token) {
            return Forbid("Wrong token");
        }
        user.Guid = Guid.NewGuid();
        db.Users.Add(user);
        db.SaveChanges();
        return Ok(user.Guid);
    }

    [HttpDelete]
    [ProducesResponseType<string>(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(
        [FromQuery] Guid token,
        [FromQuery] Guid guid
    ) {
        if (token != UserController.token) {
            return Forbid("Wrong token");
        }
        User? user = db.Users.Find(guid);
        if (user == null) {
            return NotFound();
        } else {
            db.Users.Remove(user);
            db.SaveChanges();
            return Ok();
        }
    }
}
