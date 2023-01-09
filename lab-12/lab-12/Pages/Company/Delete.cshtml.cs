using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using lab12New;

namespace lab12New.Pages.Companys
{
    public class DeleteModel : PageModel
    {
        private readonly Industrial _industialDb;

        public DeleteModel(Industrial industialDb)
        {
            _industialDb = industialDb;
        }

        [BindProperty]
        public lab12New.Company _company { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _industialDb.Companys == null)
            {
                return NotFound();
            }

            var company = await _industialDb.Companys.FirstOrDefaultAsync(m => m.Id == id);

            if (company == null)
            {
                return NotFound();
            }
            else
            {
                _company = company;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _industialDb.Companys == null)
            {
                return NotFound();
            }
            var company = await _industialDb.Companys.FindAsync(id);

            if (company != null)
            {
                _company = company;
                _industialDb.Companys.Remove(_company);
                await _industialDb.SaveChangesAsync();
            }

            return RedirectToPage("./Company");
        }
    }
}
