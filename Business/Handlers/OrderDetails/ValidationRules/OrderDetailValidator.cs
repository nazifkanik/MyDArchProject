
using Business.Handlers.OrderDetails.Commands;
using FluentValidation;

namespace Business.Handlers.OrderDetails.ValidationRules
{

    public class CreateOrderDetailValidator : AbstractValidator<CreateOrderDetailCommand>
    {
        public CreateOrderDetailValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.UnitPrice).NotEmpty();
            RuleFor(x => x.Quantity).NotEmpty();

        }
    }
    public class UpdateOrderDetailValidator : AbstractValidator<UpdateOrderDetailCommand>
    {
        public UpdateOrderDetailValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.UnitPrice).NotEmpty();
            RuleFor(x => x.Quantity).NotEmpty();

        }
    }
}