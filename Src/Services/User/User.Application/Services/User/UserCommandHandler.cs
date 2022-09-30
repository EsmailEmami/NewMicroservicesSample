using Application.Core.Repositories.EfCore;
using Domain.Core.Commands;
using User.Application.Core.User.Commands;

namespace User.Application.Services.User;

public class UserCommandHandler : ICommandHandler<CreateUserCommand, long>
{
    private readonly IEfCoreRepository<Domain.Entities.User, long> _userRepository;

    public UserCommandHandler(IEfCoreRepository<Domain.Entities.User, long> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<long> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new Domain.Entities.User().Set(request.FirstName, request.LastName, request.UserName);

        var userId = await _userRepository.InsertAndGetIdAsync(user);

        return userId;
    }
}