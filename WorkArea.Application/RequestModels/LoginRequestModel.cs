namespace WorkArea.Application.RequestModels;

public class LoginRequestModel
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string PushToken { get; set; }
    public string SecretKey { get; set; }
}