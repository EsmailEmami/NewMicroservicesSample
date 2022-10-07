using Application.Core.Repositories.EfCore;
using Domain.Core.Queries;
using User.Application.Core.Identity.Query;

namespace User.Application.Services.Identity;

public class IdentityQueryHandler : QueryHandler<GetIdentityQuery, Domain.Entities.Identity>
{
    private readonly IEfCoreRepository<Domain.Entities.Identity, Guid> _identityRepository;

    public IdentityQueryHandler(IEfCoreRepository<Domain.Entities.Identity, Guid> identityRepository)
    {
        _identityRepository = identityRepository;
    }

    public override async Task<Domain.Entities.Identity> Handle(GetIdentityQuery request, CancellationToken cancellationToken)
    {
        return await _identityRepository.GetAsync(request.Id);
    }
}