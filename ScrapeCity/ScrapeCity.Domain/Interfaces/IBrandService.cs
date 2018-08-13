using ScrapeCity.Common.Models.Brands.ViewModels;
using System.Collections.Generic;

namespace ScrapeCity.Domain.Interfaces
{
    public interface IBrandService
    {
        IEnumerable<BrandVm> GetAllBrandsVm();
        void AddBrandToDb(BrandVm bind);
        BrandVm GetBrandById(int brandId);
        void EditBrand(BrandVm bind);
        void DeleteBrand(int Id);
    }
}
