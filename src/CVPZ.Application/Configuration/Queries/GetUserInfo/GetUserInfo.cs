using CVPZ.Core;
using MediatR;
using Microsoft.Extensions.Options;

namespace CVPZ.Application.Configuration.Queries.GetUserInfo;

public class GetUserInfo : IRequest<UserInfo>
{
}

public class UserInfo
{
    public UserInfo(string displayName)
    {
        DisplayName = displayName;
    }

    public string DisplayName { get; }
}

public class GetUserInfoHandler : IRequestHandler<GetUserInfo, UserInfo>
{
    private readonly UserConfiguration _userConfiguration;

    public GetUserInfoHandler(IOptions<UserConfiguration> userConfigOptions)
    {
        this._userConfiguration = userConfigOptions.Value;
    }

    public async Task<UserInfo> Handle(GetUserInfo request, CancellationToken cancellationToken)
    {
        return await Task.Run(() => new UserInfo(_userConfiguration.YourName));
    }
}
