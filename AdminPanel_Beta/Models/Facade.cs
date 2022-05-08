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


    }
}
