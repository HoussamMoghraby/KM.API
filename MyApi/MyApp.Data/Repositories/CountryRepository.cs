using Microsoft.EntityFrameworkCore;
using MyApp.Data.Contexts;
using MyApp.Data.Models;

namespace MyApp.Data.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private UserContext _userContext;
        
        public CountryRepository(UserContext userContext)
        {
            _userContext = userContext;
        }
        public async Task<List<Country>> GetAllCountries()
        {
            var countries = await _userContext.Countries.ToListAsync();

            return countries;
        }

        public async Task<Country?> GetCountryById(int id)
        {
            var country = await _userContext.Countries.SingleOrDefaultAsync(c => c.Id == id);

            return country;
        }
    }
}
