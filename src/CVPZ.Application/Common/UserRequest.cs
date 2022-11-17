namespace CVPZ.Application.Common;

public abstract record UserRequest
{
    private Guid UserId;
    public Guid SetUserId(Guid userId) => UserId = userId;
    public Guid GetUserId() => UserId;
}
