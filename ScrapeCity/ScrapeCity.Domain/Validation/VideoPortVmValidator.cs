using FluentValidation;
using ScrapeCity.Common.Models.Monitors.MonitorProperties.ViewModels;

namespace ScrapeCity.Domain.Validation
{
    public class VideoPortVmValidator : AbstractValidator<VideoPortVm>
    {
        public VideoPortVmValidator()
        {
            RuleFor(x => x.Num).InclusiveBetween(1,4).WithMessage("The number of video ports from a certain type must be at least 1");
        }
    }
}
