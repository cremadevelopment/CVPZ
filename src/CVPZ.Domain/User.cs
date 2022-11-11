namespace CVPZ.Domain;

public class User : BaseEntity
{
    public string NickName { get; set; }
    public Guid ObjectId { get; set; }
}
