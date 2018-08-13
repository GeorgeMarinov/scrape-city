using FluentValidation;
using ScrapeCity.Common.Models.Monitors.MonitorProperties.ViewModels;

namespace ScrapeCity.Domain.Validation
{
    public class USBPortVmValidator : AbstractValidator<USBPortVm>
    {
        public USBPortVmValidator()
        {
            RuleFor(x => x.Num).InclusiveBetween(1, 10).WithMessage("The number of usb ports from a certain type must be at least 1");
        }
    }
}
