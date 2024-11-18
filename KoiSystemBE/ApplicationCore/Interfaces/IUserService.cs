using ApplicationCore.Dto;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IUserService
    {
        Task<string> RegisterAsync(RegisterUserDto registerDto);
        string Login(LoginUserDto loginDto);
        UserDto GetUserByEmail(string email);
        Task<ApiResponse<string>> UpdateUserProfileAsync(int userId, UpdateUserProfileDto updateProfileDto);
        Task<ApiResponse<ProfileDto>> GetProfile(int userId);
    }
}
