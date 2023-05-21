using API.Models;
using API.Models.DTO;

namespace API.Services.UserService
{
    public interface IUserService
    {
        public Task<ServiceResponse<IEnumerable<UserDTO>>> GetAll();
        public Task<ServiceResponse<UserDTO>> GetByDiscordId(int id);
        public Task<ServiceResponse<UserDTO>> Add(NewUserDTO newUser);
    }
}
