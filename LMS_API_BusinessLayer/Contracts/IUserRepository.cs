

using LMS_API_DataLayer.Models.DTO.AuthDto;

namespace LMS_API_BusinessLayer.Contracts
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<UserDTO> Register(RegisterationRequestDTO registerationRequestDTO);
    }
}
