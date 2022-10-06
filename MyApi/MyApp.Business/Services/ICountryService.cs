using MyApp.Data.Models;

namespace MyApp.Business.Services
{
    public interface ICountryService
    {
        public Task<List<Country>> GetCountries();
        public Task<Country?> GetCountryById(int id);
    }
}
