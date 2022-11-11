using CVPZ.Core;
using CVPZ.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CVPZ.Application.User;

public class UserVisited
{
    public record Event ( ClaimsPrincipal principal ) : INotification;

    public class Listener : INotificationHandler<Event>
    {
        private readonly CVPZContext _context;

        public Listener(CVPZContext context)
        {
            this._context = context;
        }

        public async Task Handle(Event notification, CancellationToken cancellationToken)
        {
            var objectId = notification.principal.GetClaim("http://schemas.microsoft.com/identity/claims/objectidentifier");
            
            var user = await _context.Users.FirstOrDefaultAsync(x => x.ObjectId == objectId);
            if (null == user)
            {
                user = new Domain.User
                {
                    NickName = notification.principal.GetClaim("name"),
                    ObjectId = objectId
                };
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
