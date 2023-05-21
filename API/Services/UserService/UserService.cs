using API.Data;
using API.Models;
using API.Models.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services.UserService
{
    public class UserService : IUserService
    {
        
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;

        public UserService(UserDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            
        }

        public async Task<ServiceResponse<UserDTO>> Add(NewUserDTO newUser)
        {
            ServiceResponse<UserDTO> serviceResponse = new(null);

            User? user = await _context.Users.FirstOrDefaultAsync(user => user.DiscordId == newUser.DiscordId);
            if (user != null)
            {
                serviceResponse.Code = StatusCodes.Status409Conflict;
                serviceResponse.Message = "User with that Discord ID already exists!";
                return serviceResponse;
            }

            user = _mapper.Map<User>(newUser);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            serviceResponse.Code = StatusCodes.Status200OK;
            serviceResponse.Message = "User successfully registered.";
            serviceResponse.Data = _mapper.Map<UserDTO>(user);

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<UserDTO>>> GetAll()
        {
            List<UserDTO> users = _mapper.Map<List<UserDTO>>(await _context.Users.ToListAsync());

            ServiceResponse<List<UserDTO>> serviceResponse = new(users, 200, "Fetched all existing users.");
            return serviceResponse;
        }

        public async Task<ServiceResponse<UserDTO>> GetByDiscordId(int discordId)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(user => user.DiscordId == discordId);
            ServiceResponse<UserDTO> serviceResponse = new(_mapper.Map<UserDTO>(user));
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
