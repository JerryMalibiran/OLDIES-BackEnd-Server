using API.Data;
using API.Models;
using API.Models.DTO;
using API.Models.Response;
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

        public async Task<IResponse> Add(NewUserDTO newUser)
        {
            IResponse response;

            User? user = await _context.Users.FirstOrDefaultAsync(user => user.DiscordId == newUser.DiscordId);
            if (user != null)
            {
                response = new DefaultResponse(StatusCodes.Status409Conflict, "User with that Discord ID already exists!");
                return response;
            }

            user = _mapper.Map<User>(newUser);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            response = new DataResponse<UserDTO>(
                _mapper.Map<UserDTO>(user),
                StatusCodes.Status200OK,
                "User successfully registered."
                );

            return response;
        }

        public async Task<IDataResponse<IEnumerable<UserDTO>>> GetAll()
        {
            IEnumerable<UserDTO> users = _mapper.Map<List<UserDTO>>(await _context.Users.ToListAsync());

            IDataResponse<IEnumerable<UserDTO>> response = new DataResponse<IEnumerable<UserDTO>>(users, 200, "Fetched all existing users.");
            return response;
        }

        public async Task<IResponse> GetByDiscordId(int discordId)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(user => user.DiscordId == discordId);
            if (user == null)
            {
                return new DefaultResponse(StatusCodes.Status404NotFound, "User not found.");
            }

            return new DataResponse<UserDTO>(_mapper.Map<UserDTO>(user), StatusCodes.Status200OK, "User found.");
        }
    }
}
