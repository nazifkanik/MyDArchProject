
using Business.Handlers.Orders.Commands;
using FluentValidation;

namespace Business.Handlers.Orders.ValidationRules
{

    public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.EmployeeId).NotEmpty();
            RuleFor(x => x.ShipperId).NotEmpty();
            RuleFor(x => x.OrderDate).NotEmpty();
            RuleFor(x => x.RequiredDate).NotEmpty();
            RuleFor(x => x.ShippedDate).NotEmpty();
            RuleFor(x => x.Freight).NotEmpty();
            RuleFor(x => x.ShipName).NotEmpty();
            RuleFor(x => x.ShipAddress).NotEmpty();
            RuleFor(x => x.ShipCity).NotEmpty();
            RuleFor(x => x.ShipRegion).NotEmpty();
            RuleFor(x => x.ShipPostalCode).NotEmpty();
            RuleFor(x => x.ShipCountry).NotEmpty();

        }
    }
    public class UpdateOrderValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.EmployeeId).NotEmpty();
            RuleFor(x => x.ShipperId).NotEmpty();
            RuleFor(x => x.OrderDate).NotEmpty();
            RuleFor(x => x.RequiredDate).NotEmpty();
            RuleFor(x => x.ShippedDate).NotEmpty();
            RuleFor(x => x.Freight).NotEmpty();
            RuleFor(x => x.ShipName).NotEmpty();
            RuleFor(x => x.ShipAddress).NotEmpty();
            RuleFor(x => x.ShipCity).NotEmpty();
            RuleFor(x => x.ShipRegion).NotEmpty();
            RuleFor(x => x.ShipPostalCode).NotEmpty();
            RuleFor(x => x.ShipCountry).NotEmpty();

        }
    }
}