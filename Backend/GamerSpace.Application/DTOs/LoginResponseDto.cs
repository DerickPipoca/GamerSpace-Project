namespace GamerSpace.Application.DTOs
{
    public class LoginResponseDto(string token)
    {
        public string Token { get; set; } = token;
    }
}