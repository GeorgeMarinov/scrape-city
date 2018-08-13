namespace ScrapeCity.Domain.Interfaces
{
    public interface IAlgoliaMonitorIndex
    {
        void AddItem(int Id);
        void DeleteItem(int Id);
        void EditItem(int Id);
        void UpdateMonitorIndex();
    }
}
