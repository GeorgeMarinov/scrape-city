 using ScrapeCity.Data;

namespace ScrapeCity.Domain
{
    public abstract class AbstractService
    {
        protected ScrapeCityDbContext context;
        //TODO resolve dependancy
        public AbstractService()
        {
            this.context = new ScrapeCityDbContext();
        }
    }
}
