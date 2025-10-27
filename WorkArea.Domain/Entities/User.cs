using WorkArea.Domain.Entities.Base;

namespace WorkArea.Domain.Entities;

public class User :  BaseEntityWithDate
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Fullname { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Avatar { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PushToken { get; set; } = string.Empty;
}