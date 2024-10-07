namespace Domain.Users;

public class UserToken : Entity
{
    public Guid UserId { get; set; }
    public string Token { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ExpireDate { get; set; }
}