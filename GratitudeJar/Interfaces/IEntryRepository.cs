namespace GratitudeJar.Interfaces;

public interface IEntryRepository
{
    Task<List<GratitudeJar.Models.Entry>> GetAll(int userId);
    Task<GratitudeJar.Models.Entry?> GetById(int id, int userId);
    Task Add(GratitudeJar.Models.Entry entry);
    Task Update(GratitudeJar.Models.Entry entry);
    Task Delete(int id, int userId);
    string GetEntryType(GratitudeJar.Models.Entry entry);
}
