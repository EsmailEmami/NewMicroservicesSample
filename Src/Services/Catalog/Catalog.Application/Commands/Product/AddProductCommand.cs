using Domain.Core.Commands;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Catalog.Application.Commands.Product;

public class AddProductCommand :  ICommand<Guid>
{
    public Domain.Entities.Product Product { get; private set; }
    public AddProductCommand(Domain.Entities.Product product)
    {
        Product = product;
    }

    public bool IsValid()
    {
        return true;
    }

    public DateTime Timestamp { get; }
    public ValidationResult ValidationResult { get; set; }
}

public class Validator : AbstractValidator<AddProductCommand>
{
    public Validator()
    {
        RuleFor(cmd => cmd.Product).NotNull();
    }
}