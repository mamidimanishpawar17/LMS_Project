namespace LMS_WEB.Models.DTO.AuthDto
{
    public class LoginResponseDTO
    {
        public UserDTO User { get; set; }
        public string Token { get; set; }
    }
}
