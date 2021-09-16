
using Business.Handlers.Regions.Commands;
using FluentValidation;

namespace Business.Handlers.Regions.ValidationRules
{

    public class CreateRegionValidator : AbstractValidator<CreateRegionCommand>
    {
        public CreateRegionValidator()
        {
            RuleFor(x => x.RegionDescription).NotEmpty();

        }
    }
    public class UpdateRegionValidator : AbstractValidator<UpdateRegionCommand>
    {
        public UpdateRegionValidator()
        {
            RuleFor(x => x.RegionDescription).NotEmpty();

        }
    }
}