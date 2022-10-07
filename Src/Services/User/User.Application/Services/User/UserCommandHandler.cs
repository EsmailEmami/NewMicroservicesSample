using Application.Core.Repositories.EfCore;
using Domain.Core.Commands;
using User.Application.Core.User.Commands;

namespace User.Application.Services.User;

public class UserCommandHandler : CommandHandler<CreateUserCommand, long>
{
    private readonly IEfCoreRepository<Domain.Entities.User, long> _userRepository;

    public UserCommandHandler(IEfCoreRepository<Domain.Entities.User, long> userRepository)
    {
        _userRepository = userRepository;
    }


    public override async Task<long> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        CheckValidation(request);

        var user = new Domain.Entities.User(request.FirstName, request.LastName, request.UserName, request.Password);
        var userIdentiy = user.Identiy;
        var userId = await _userRepository.InsertAndGetIdAsync(user);

        return userId;
    }
}