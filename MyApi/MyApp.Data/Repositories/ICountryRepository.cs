using MyApp.Data.Models;

namespace MyApp.Data.Repositories
{
    public interface ICountryRepository
    {
        public Task<List<Country>> GetAllCountries();
        public Task<Country?> GetCountryById(int id);
    }
}
