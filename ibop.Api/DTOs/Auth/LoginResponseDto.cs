using ibop.Api.DTOs.Auth;

namespace ibop.Api.DTOs.Auth
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = default!;
        public string Role { get; set; } = default!;
        public UserDto User { get; set; } = default!;
    }

}
}
