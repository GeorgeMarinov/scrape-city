namespace ScrapeCity.Common.Models.Brands.ViewModels
{
    public class BrandVm
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public bool CanBeDeleted { get; set; } = true;
    }
}
