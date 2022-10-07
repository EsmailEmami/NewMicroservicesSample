using Application.Core.Repositories.EfCore;
using Domain.Core.Queries;
using Domain.Exceptions;
using User.Application.Core.User.Queries;

namespace User.Application.Services.User;

public class UserQueryHandler : QueryHandler,
    IQueryHandler<GetUserByUserNameQuery, Domain.Entities.User>,
    IQueryHandler<GetUserByUserIdQuery, Domain.Entities.User>
{
    private readonly IEfCoreRepository<Domain.Entities.User, long> _userRepository;

    public UserQueryHandler(IEfCoreRepository<Domain.Entities.User, long> userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<Domain.Entities.User> Handle(GetUserByUserNameQuery request, CancellationToken cancellationToken)
    {
        CheckValidation(request);
        var user = _userRepository.FirstOrDefault(x => x.UserName == request.UserName);
        if (user == null)
            throw new EntityNotFoundException("کاربر یافت نشد");

        return Task.FromResult(user);
    }

    public Task<Domain.Entities.User> Handle(GetUserByUserIdQuery request, CancellationToken cancellationToken)
    {
        CheckValidation(request);
        var user = _userRepository.Get(request.UserId);
        return Task.FromResult(user);
    }
}