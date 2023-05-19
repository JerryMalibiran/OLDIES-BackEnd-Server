using API.Models;
using API.Models.DTO;

namespace API.Services.UserService
{
    public interface IUserService
    {
        public Task<ServiceResponse<List<User>>> GetAll();
        public Task<ServiceResponse<User>> GetByDiscordId(int id);
        public Task<ServiceResponse<User>> Add(NewUserDTO newUser);
    }
}
