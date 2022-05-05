using CVPZ.Infrastructure.Data;
using MediatR;

namespace CVPZ.Application.Job.Commands.CreateJob;

public class CreateJobHandler : IRequestHandler<CreateJobRequest, CreateJobResponse>
{
    private readonly CVPZContext _context;

    public CreateJobHandler(CVPZContext context)
    {
        this._context = context;
    }

    public async Task<CreateJobResponse> Handle(CreateJobRequest request, CancellationToken cancellationToken)
    {
        var entity = await MapToEntity(request);
        await PersistEntity(entity);

        return new CreateJobResponse(entity.Id.ToString(), entity.Title);
    }

    private async Task<Domain.Job> MapToEntity(CreateJobRequest request)
    {
        return new Domain.Job {  Title = request.Title };
    }

    private async Task PersistEntity (Domain.Job entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
    }
}
