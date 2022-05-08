#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AdminPanel_Beta.Models;

namespace AdminPanel_Beta.Pages.FResultGroups
{
    public class IndexModel : PageModel
    {
        private readonly AdminPanel_Beta.Models.AssessmentDBContext _context;

        public IndexModel(AdminPanel_Beta.Models.AssessmentDBContext context)
        {
            _context = context;
        }

        public IList<AssessmentResultGroup> AssessmentResultGroup { get;set; }

        public async Task OnGetAsync()
        {
            AssessmentResultGroup = await _context.AssessmentResultGroups.ToListAsync();
        }
    }
}
