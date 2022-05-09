using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

        public AssessmentResultGroup GetResultGroup(int id)
        {
            return context.AssessmentResultGroups.Where(s => s.Id == id
                                                             && s.IsActive)
                .Include(s => 
                    s.ResultTips.Where(a=>a.IsActive))
                .First();
        }

        public List<ResultTip> GetTips(int? groupId)
        {
            return context.ResultTips.Where(s => (groupId== null ||
                                                  s.AssessmentResultGroupId == groupId)
                                                 && s.IsActive)
                .Include(s=>s.AssessmentResultGroup)
                .ToList();
        }

        public List<Subject> GetSubjects(string keyword)
        {
            return context.Subjects.Where(s => s.IsActive!.Value == true &&
                                               (string.IsNullOrEmpty(keyword) ||
                                              
                                                 s.Title.Contains(keyword) ||
                                                 s.FirstQuestion.Code.Contains(keyword)))
                .Include(s => s.FirstQuestion)
                .ToList();
        }

        private List<Question>? _questionCache = null;

        private List<Question> getQuestions()
        {
            if (_questionCache != null)
                return _questionCache;
            
            _questionCache = context.Questions.ToList();
            return _questionCache;
        }
        public List<Question> QuestionLookup(string keyword)
        {
            
            return getQuestions().Where(s => s.IsActive.Value
                                           && (string.IsNullOrEmpty(keyword) ||
                                               s.Code.Contains(keyword) ||
                                               s.Body.Contains(keyword)))
                .Take(5).ToList();
        }

    }
}
