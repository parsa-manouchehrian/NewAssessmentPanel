using Microsoft.AspNetCore.Identity;

namespace AdminPanel_Beta.Data
{
    public class Admin : IdentityUser
    {
        public string? NoteOnProfile { get; set; }
    }
}
