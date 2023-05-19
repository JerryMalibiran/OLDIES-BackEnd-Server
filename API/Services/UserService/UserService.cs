

using API.Data;
using API.Models;
using API.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace API.Services.UserService
{
    public class UserService : IUserService
    {
        /*private static List<User> _users = new()
        {
            new User(1, 1),
            new User(2, 2),
            new User(3, 3)
        };*/
        
        private readonly UserContext _context;
        public UserService(UserContext context)
        {
            _context = context;
            
        }

        public async Task<ServiceResponse<User>> Add(NewUserDTO newUser)
        {
            ServiceResponse<User> serviceResponse = new(null);

            User? user = await _context.Users.FirstOrDefaultAsync(user => user.DiscordId == newUser.DiscordId);
            if (user != null)
            {
                serviceResponse.Code = StatusCodes.Status409Conflict;
                serviceResponse.Message = "User with that Discord ID already exists!";
                return serviceResponse;
            }

            user = new(newUser.DiscordId);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            serviceResponse.Code = StatusCodes.Status200OK;
            serviceResponse.Message = "User successfully registered.";
            serviceResponse.Data = user;

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<User>>> GetAll()
        {
            ServiceResponse<List<User>> serviceResponse = new(await _context.Users.ToListAsync(), 200, "Fetched all existing users.");
            return serviceResponse;
        }

        public async Task<ServiceResponse<User>> GetByDiscordId(int discordId)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(user => user.DiscordId == discordId);
            ServiceResponse<User> serviceResponse = new(user);
            if (user != null)
            {
                serviceResponse.Code = StatusCodes.Status200OK;
                serviceResponse.Message = "User found.";
            } else
            {
                serviceResponse.Code = StatusCodes.Status404NotFound;
                serviceResponse.Message = "User not found.";
            }

            return serviceResponse;
        }
    }
}
