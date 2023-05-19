using API.Models;
using API.Models.Dto;

namespace API.Services.UserService
{
    public interface IUserService
    {
        public Task<ServiceResponse<List<User>>> GetAll();
        public Task<ServiceResponse<User>> GetByDiscordID(int id);
        public Task<ServiceResponse<User>> Add(NewUserDto newUser);
    }
}
