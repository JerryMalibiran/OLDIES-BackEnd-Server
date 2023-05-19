using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Models.Dto;
using API.Services.UserService;

namespace API.Controllers
{
    [Route("oldies/api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;

        public UsersController(ILogger<UsersController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet(Name = "GetUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ServiceResponse<List<User>>>> GetUsers() {
            ServiceResponse<List<User>> response = await _userService.GetAll();
            return StatusCode(response.Code, response);
        }

        [HttpGet("{discordID:int}", Name = "GetUser")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ServiceResponse<User>>> GetUser(int discordID)
        {
            ServiceResponse<User> response = await _userService.GetByDiscordID(discordID);
            return StatusCode(response.Code, response);
        }

        [HttpPost(Name = "AddUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<ServiceResponse<User>>> AddUser([FromBody]NewUserDto newUser)
        {
            ServiceResponse<User> response = await _userService.Add(newUser);
            return StatusCode(response.Code, response);
        }
       
    }
}
