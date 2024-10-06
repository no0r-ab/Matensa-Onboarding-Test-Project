namespace Application.Services.Users;

public class UserTokernResult
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Token { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ExpireDate { get; set; }
}
