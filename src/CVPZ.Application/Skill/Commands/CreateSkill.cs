using CVPZ.Domain;

namespace CVPZ.Application.Skill.Commands;

public class CreateSkill
{
    public record Request(string Category, string Name, string Description, SkillLevel Level) : IRequest<OneOf<Response, Error>>;
    public record Response(string Id, string Category, string Name, string Description, SkillLevel Level);

    public class Errors
    {
        public static Error CategoryRequired => new(Code: nameof(CategoryRequired), "Skill category is required");
        public static Error NameRequired => new(Code: nameof(NameRequired), "Skill name is required");
        public static Error SkillsDoNotGoToEleven => new(Code: nameof(SkillsDoNotGoToEleven), "Skillz do not go to 11!");
    }

    public class Handler : IRequestHandler<Request, OneOf<Response, Error>>
    {
        private readonly CVPZContext _context;

        public Handler(CVPZContext context)
        {
            this._context = context;
        }

        public async Task<OneOf<Response, Error>> Handle(Request request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Category))
                return Errors.CategoryRequired;

            if (string.IsNullOrEmpty(request.Name))
                return Errors.NameRequired;

            var entity = await MapToEntity(request);
            await PersistEntity(entity);

            return new Response(
                entity.Id.ToString(),
                entity.Category,
                entity.Name,
                entity.Description,
                entity.Level
            );
        }

        private async Task<Domain.Skill> MapToEntity(Request request)
        {
            return new Domain.Skill
            {
                Category = request.Category,
                Name = request.Name,
                Description = request.Description,
                Level = request.Level
            };
        }

        private async Task PersistEntity(Domain.Skill entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }
}
