

using API.Models;
using API.Models.Dto;

namespace API.Services.UserService
{
    public class UserService : IUserService
    {
        private static List<User> _users = new()
        { 
            new User(1, 1),
            new User(2, 2),
            new User(3, 3)
        };   

        public async Task<ServiceResponse<User>> Add(NewUserDto newUser)
        {
            ServiceResponse<User> serviceResponse = new(null);
            User user = _users.FirstOrDefault(user => user.DiscordID == newUser.DiscordID);

            if (user != null)
            {
                serviceResponse.Code = StatusCodes.Status409Conflict;
                serviceResponse.Message = "User with that Discord ID already exists!";
                return serviceResponse;
            }


            int id = _users.OrderByDescending(user => user.ID).FirstOrDefault().ID + 1;
            user = new(id, newUser.DiscordID);
            _users.Add(user);

            serviceResponse.Code = StatusCodes.Status200OK;
            serviceResponse.Message = "User successfully registered.";
            serviceResponse.Data = user;

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<User>>> GetAll()
        {
            ServiceResponse<List<User>> serviceResponse = new(_users, 200, "Fetched all existing users.");
            return serviceResponse;
        }

        public async Task<ServiceResponse<User>> GetByDiscordID(int discordID)
        {
            User user = _users.FirstOrDefault(user => user.DiscordID == discordID);
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
