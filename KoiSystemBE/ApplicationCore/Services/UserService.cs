using ApplicationCore.Dto;
using ApplicationCore.Interfaces;
using ApplicationCore.Repositories;
using AutoMapper;
using Infrastructure.Models;
using Infrastructure.Settings;
using Infrastructure.Utils;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRepository<User> _repository;
        private readonly IMapper _mapper;
        private readonly AuthenticationHelper _authHelper;

        public UserService(IUserRepository userRepository, IRepository<User> repository, IMapper mapper, IOptions<JwtSettings> jwtSettings)
        {
            _userRepository = userRepository;
            _repository = repository;
            _mapper = mapper;
            _authHelper = new AuthenticationHelper(jwtSettings.Value);
        }

        public async Task<string> RegisterAsync(RegisterUserDto registerDto)
        {
            var existingUser = _userRepository.GetByEmail(registerDto.Email);
            if (existingUser != null)
            {
                throw new Exception("Email is already registered.");
            }
            var user = new User
            {
                Email = registerDto.Email,
                Name = registerDto.Name,
                Phone = registerDto.PhoneNumber,
                Address = registerDto.Address,
                PasswordHash = AuthenticationHelper.HashPassword(registerDto.Password),
                RoleId = 1 
            };

            await _repository.AddAsync(user);
            var userLogin = _userRepository.GetByEmail(registerDto.Email); 
            return _authHelper.GenerateJwtToken(userLogin); 
        }

        public string Login(LoginUserDto loginDto)
        {
            var user = _userRepository.GetByEmail(loginDto.Email);
            if (user == null || !AuthenticationHelper.VerifyPassword(loginDto.Password, user.PasswordHash))
            {
                throw new Exception("Invalid login credentials.");
            }

            return _authHelper.GenerateJwtToken(user);
        }

        public UserDto GetUserByEmail(string email)
        {
            var user = _userRepository.GetByEmail(email);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            return new UserDto
            {
                Email = user.Email,
                Name = user.Name,
                PhoneNumber = user.Phone,
                Address = user.Address
            };
        }

        public async Task<ApiResponse<string>> UpdateUserProfileAsync(int userId, UpdateUserProfileDto updateProfileDto)
        {
            var user = await _repository.GetByIdAsync(userId);

            if (user == null)
            {
                return ApiResponse<string>.ErrorResponse("User not found.");
            }

            user.Name = updateProfileDto.Name;
            user.Phone = updateProfileDto.Phone;
            user.Address = updateProfileDto.Address;

            await _repository.UpdateAsync(user); // Update the user details

            return ApiResponse<string>.SuccessResponse("User profile updated successfully.");
        }

        public async Task<ApiResponse<ProfileDto>> GetProfile(int userId)
        {
            var user = await _repository.GetByIdAsync(userId);

            if (user == null)
            {
                return ApiResponse<ProfileDto>.ErrorResponse("User not found.");
            }

            var profileDto = _mapper.Map<ProfileDto>(user);
            return ApiResponse<ProfileDto>.SuccessResponse(profileDto, "Profile retrieved successfully.");
        }
    }
}
