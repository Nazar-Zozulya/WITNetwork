using WITnetwork.Dtos;

public interface IAuthService
{
    Task<string> Create(CreateDto dto);

    Task<string> Login(LoginDto dto);

    Task<string> PreConfirmEmail(PreConfirmEmailDto dto);

    Task<string> ConfirmEmail(ConfirmEmailDto dto);
}


