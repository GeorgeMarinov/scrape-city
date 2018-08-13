using FluentValidation;
using ScrapeCity.Common.Models.Monitors.ViewModels;

namespace ScrapeCity.Domain.Validation
{
    public class MonitorVmValidator : AbstractValidator<MonitorVm>
    {
        public MonitorVmValidator()
        {
            //RuleFor(x => x.MonitorURL)
            //    .NotEmpty()
            //    .Matches(@"^((http[s]?|ftp):\/)?\/?([^:\/\s]+)((\/\w+)*\/)([\w\-\.]+[^#?\s]+)(.*)?(#[\w\-]+)?$");
            //RuleFor(x => x.Model).NotEmpty();
            //RuleFor(x => x.Brightness).InclusiveBetween(1, 999999);
            //RuleFor(x => x.ResponseTime).InclusiveBetween(1, 100);
            //RuleFor(x => x.ScreenSize).InclusiveBetween(1,150);
            //RuleFor(x => x.ColorDepth).InclusiveBetween(8, 10);
            //RuleFor(x => x.MaxHorizontalPixels).InclusiveBetween(100, 9999999);
            //RuleFor(x => x.MaxVerticalPixels).InclusiveBetween(100, 9999999);
            //RuleFor(x => x.MaxFrequencyAtMaxResolution).InclusiveBetween(30, 400);
            RuleFor(x => x.VideoPorts).Must(x=>x.Count > 0).WithMessage("Monitors must have at least 1 video port");
            RuleFor(x => x.VideoPorts).SetCollectionValidator(new VideoPortVmValidator());
            RuleFor(x => x.USBPorts).SetCollectionValidator(new USBPortVmValidator());
        }
    }
}
