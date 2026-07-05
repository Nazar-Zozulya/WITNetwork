using WITnetwork.Dtos;

public interface IAuthService
{
    Task<string> Register(RegisterDto dto);

    Task<string> Login(LoginDto dto);

    Task<string> PreConfirmEmail(PreConfirmEmailDto dto);

    Task<string> ConfirmEmail(ConfirmEmailDto dto);
}


