using API.Models.DTO;
using API.Models.Response;

namespace API.Services.UserService
{
    public interface IUserService
    {
        public Task<IDataResponse<IEnumerable<UserDTO>>> GetAll();
        public Task<IResponse> GetByDiscordId(int id);
        public Task<IResponse> Add(NewUserDTO newUser);
    }
}
