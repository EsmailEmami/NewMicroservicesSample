using Domain.Core.Commands;
using FluentValidation;
using MediatR;

namespace Catalog.Application.Commands.Product;

public class AddProductCommand :  ICommand<Guid>
{
    public Domain.Entities.Product Product { get; private set; }
    public AddProductCommand(Domain.Entities.Product product)
    {
        Product = product;
    }
}

public class Validator : AbstractValidator<AddProductCommand>
{
    public Validator()
    {
        RuleFor(cmd => cmd.Product).NotNull();
    }
}