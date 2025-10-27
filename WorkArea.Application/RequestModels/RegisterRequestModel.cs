namespace WorkArea.Application.RequestModels;

public class RegisterRequestModel
{
    public int? Id { get; set; }
    public string? Fullname { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? RPassword { get; set; }
    public string? Username { get; set; }
    public string? SecretKey { get; set; }
    public string? PhoneNumber { get; set; }
    public string? PushToken { get; set; }
    public bool IsEmailContactApproved { get; set; } = false;
}