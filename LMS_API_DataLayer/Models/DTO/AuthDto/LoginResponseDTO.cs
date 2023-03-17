namespace LMS_API_DataLayer.Models.DTO.AuthDto
{
    public class LoginResponseDTO
    {
        public UserDTO User { get; set; }
        public string Token { get; set; }
    }
}
