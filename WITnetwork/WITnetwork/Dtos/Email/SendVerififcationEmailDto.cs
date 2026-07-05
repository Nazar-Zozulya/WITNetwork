namespace WITnetwork.Dtos;


public class SendVerififcationEmailDto
{   
    public string Email { get; set; } = null!;
    public string VerificationCode { get; set; } = null!;
}