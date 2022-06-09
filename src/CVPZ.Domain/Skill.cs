namespace CVPZ.Domain;

public class Skill : BaseEntity
{
    public string Category { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public SkillLevel Level { get; set; }
}
