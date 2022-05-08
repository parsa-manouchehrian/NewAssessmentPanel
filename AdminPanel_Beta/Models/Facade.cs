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
        
        
        public List<User> GetUsers(string keyword,
            string status,
            bool hasAccepted,
            bool hasNotAccepted,
            int provinceId)
        {
            return context.Users
                .Where(s => s.IsActive &&
                            (string.IsNullOrEmpty(keyword) || 
                             s.Firstname.Contains(keyword) ||
                             s.Lastname.Contains(keyword) ||
                             s.Phone.Contains(keyword) ||
                             s.Email.Contains(keyword) ) &&
                            (provinceId == -1 || 
                             s.ProvinceId == provinceId) &&
                            (string.IsNullOrEmpty(status) ||
                             s.UserStatusName.Equals(status)) &&
                            (s.HasAcceptAgreement == hasAccepted || 
                             s.HasAcceptAgreement == !hasNotAccepted ))
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


        public List<AssessmentResultGroup> GetGroups(string keyword)
        {
            return context.AssessmentResultGroups
                .Where(s => s.IsActive
                && (string.IsNullOrEmpty(keyword) || (s.Title != null && s.Title.Contains(keyword))
                                                  || (s.Description != null && s.Description.Contains(keyword))))
                .ToList();
        }

    }
}
