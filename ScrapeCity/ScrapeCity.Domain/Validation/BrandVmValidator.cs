using FluentValidation;
using ScrapeCity.Common.Models.Brands.ViewModels;

namespace ScrapeCity.Domain.Validation
{
    public class BrandVmValidator : AbstractValidator<BrandVm>
    {
        public BrandVmValidator()
        {
            RuleFor(x => x.BrandName).NotEmpty();
        }
    }
}
