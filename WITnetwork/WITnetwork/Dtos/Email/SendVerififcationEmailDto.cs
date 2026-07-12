namespace WITnetwork.Dtos;


public class SendVerificationEmailDto
{   
    public string Email { get; set; } = null!;
    public string VerificationCode { get; set; } = null!;
}