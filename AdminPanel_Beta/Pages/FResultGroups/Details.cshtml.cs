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
    public class DetailsModel : PageModel
    {
        private readonly AdminPanel_Beta.Models.AssessmentDBContext _context;

        public DetailsModel(AdminPanel_Beta.Models.AssessmentDBContext context)
        {
            _context = context;
        }

        public AssessmentResultGroup AssessmentResultGroup { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AssessmentResultGroup = await _context.AssessmentResultGroups.FirstOrDefaultAsync(m => m.Id == id);

            if (AssessmentResultGroup == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
