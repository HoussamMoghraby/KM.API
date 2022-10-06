using MyApp.Data.Models;
using MyApp.Data.Repositories;

namespace MyApp.Business.Services
{
    public class CountryService : ICountryService
    {
        private ICountryRepository _countryRepository;
        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }
        public async Task<List<Country>> GetCountries()
        {
            var countries = await _countryRepository.GetAllCountries();

            return countries;
        }

        public async Task<Country?> GetCountryById(int id)
        {
            var country = await _countryRepository.GetCountryById(id);

            return country;
        }
    }
}
