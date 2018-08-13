using AutoMapper;
using ScrapeCity.Common.Models.Brands;
using ScrapeCity.Common.Models.Brands.ViewModels;
using ScrapeCity.Domain.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ScrapeCity.Domain.BrandServices
{
    public class BrandService : AbstractService, IBrandService
    {
        public IEnumerable<BrandVm> GetAllBrandsVm()
        {
            var allBrands = context.Brands.ToList();
            var vms = Mapper.Map<IEnumerable<Brand>, IEnumerable<BrandVm>>(allBrands);
            foreach (var vm in vms)
            {
                if (context.Monitors.Any(x => x.Brand.Id == vm.Id))
                {
                    vm.CanBeDeleted = false;
                }
            }
            return vms;
        }

        public void AddBrandToDb(BrandVm bind)
        {
            var brand = Mapper.Map<BrandVm, Brand>(bind);
            context.Brands.Add(brand);
            context.SaveChanges();
        }

        public BrandVm GetBrandById(int Id)
        {
            var brand = context.Brands.Find(Id);
            var vm = Mapper.Map<Brand, BrandVm>(brand);
            if (context.Monitors.Any(x => x.Brand.Id == vm.Id))
            {
                vm.CanBeDeleted = false;
            }
            return vm;
        }

        public void EditBrand(BrandVm bind)
        {
            var brand = context.Brands.Find(bind.Id);

            brand = Mapper.Map<BrandVm, Brand>(bind, brand);

            context.Entry(brand).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteBrand(int Id)
        {
            var brand = context.Brands.Find(Id);
            if (brand != null)
            {
                context.Brands.Remove(brand);
                context.SaveChanges();
            }
        }
    }
}
