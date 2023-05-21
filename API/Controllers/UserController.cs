using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Models.DTO;
using API.Services.UserService;
using System.Collections;

namespace API.Controllers
{
    [Route("oldies/api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet(Name = "GetUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ServiceResponse<IEnumerable<UserDTO>>>> GetUsers() {
            ServiceResponse<IEnumerable<UserDTO>> response = await _userService.GetAll();
            return StatusCode(response.Code, response);
        }

        [HttpGet("{discordId:int}", Name = "GetUser")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ServiceResponse<UserDTO>>> GetUser(int discordId)
        {
            ServiceResponse<UserDTO> response = await _userService.GetByDiscordId(discordId);
            return StatusCode(response.Code, response);
        }

        [HttpPost(Name = "AddUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<ServiceResponse<UserDTO>>> AddUser([FromBody]NewUserDTO newUser)
        {
            ServiceResponse<UserDTO> response = await _userService.Add(newUser);
            return StatusCode(response.Code, response);
        }
       
    }
}
