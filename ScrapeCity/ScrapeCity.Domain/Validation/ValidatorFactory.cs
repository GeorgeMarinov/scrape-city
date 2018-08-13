using System;
using System.Collections.Generic;
using FluentValidation;
using ScrapeCity.Common.Models.Monitors.ViewModels;
using ScrapeCity.Common.Models.Brands.ViewModels;

namespace ScrapeCity.Domain.Validation
{
    public class ValidatorFactory : ValidatorFactoryBase
    {
        private static Dictionary<Type, IValidator> validators = new Dictionary<Type, IValidator>();

        static ValidatorFactory()
        {
            validators.Add(typeof(IValidator<MonitorVm>), new MonitorVmValidator());
            validators.Add(typeof(IValidator<BrandVm>), new BrandVmValidator());
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            IValidator validator;
            if (validators.TryGetValue(validatorType, out validator))
            {
                return validator;
            }
            return validator;
        }
    }
}