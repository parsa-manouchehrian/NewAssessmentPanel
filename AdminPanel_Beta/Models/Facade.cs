using Microsoft.EntityFrameworkCore;

namespace AdminPanel_Beta.Models
{
    public class Facade
    {
        public AssessmentDBContext context;

        public Facade(AssessmentDBContext context)
        {
            this.context = context;
        }

        public List<User> GetUsers()
        {
            return context.Users
                .Where(s => s.IsActive)
                .Include(s => s.Province)
                .ThenInclude(s => s.Country)
                .Include(s => s.UserStatusNameNavigation)
                .ToList();
        }
        
        
        public List<Country> GetCountriesAndProvinces()
        {
            return context.Countries
                .Include(s => s.Provinces)
                .ToList();
            
        }

        public List<UserStatus> GetStatuses()
        {
            return context.UserStatuses.ToList();
        }


    }
}
