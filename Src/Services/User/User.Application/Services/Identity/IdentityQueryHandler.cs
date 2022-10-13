using Application.Core.Repositories.EfCore;
using Domain.Core.Queries;
using User.Application.Core.Identity.Query;

namespace User.Application.Services.Identity;

public class IdentityQueryHandler : QueryHandler,
    IQueryHandler<GetIdentityQuery, Domain.Entities.Identity>
{
    private readonly IEfCoreRepository<Domain.Entities.User, long> _userRepository;

    public IdentityQueryHandler(IEfCoreRepository<Domain.Entities.User, long> userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<Domain.Entities.Identity> Handle(GetIdentityQuery request, CancellationToken cancellationToken)
    {
        CheckValidation(request);
        return Task.FromResult(_userRepository.Get(request.UserId).Identiy);
    }
}