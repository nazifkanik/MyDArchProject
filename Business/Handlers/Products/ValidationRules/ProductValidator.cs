
using Business.Handlers.Products.Commands;
using FluentValidation;

namespace Business.Handlers.Products.ValidationRules
{

    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.CategoryId).NotEmpty();
            RuleFor(x => x.SupplierId).NotEmpty();
            RuleFor(x => x.ProductName).NotEmpty();
            RuleFor(x => x.QuantityPerUnit).NotEmpty();
            RuleFor(x => x.UnitPrice).NotEmpty();
            RuleFor(x => x.UnitsInStock).NotEmpty();
            RuleFor(x => x.UnitsOnOrder).NotEmpty();
            RuleFor(x => x.ReorderLevel).NotEmpty();

        }
    }
    public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.CategoryId).NotEmpty();
            RuleFor(x => x.SupplierId).NotEmpty();
            RuleFor(x => x.ProductName).NotEmpty();
            RuleFor(x => x.QuantityPerUnit).NotEmpty();
            RuleFor(x => x.UnitPrice).NotEmpty();
            RuleFor(x => x.UnitsInStock).NotEmpty();
            RuleFor(x => x.UnitsOnOrder).NotEmpty();
            RuleFor(x => x.ReorderLevel).NotEmpty();

        }
    }
}