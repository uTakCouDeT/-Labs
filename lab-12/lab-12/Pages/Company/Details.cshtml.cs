using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using lab12New;

namespace lab12New.Pages.Companys
{
    public class DetailsModel : PageModel
    {
        private readonly Industrial _industialDb;

        public DetailsModel(Industrial industialDb)
        {
            _industialDb = industialDb;
        }

        public lab12New.Company _company { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _industialDb.Companys == null)
            {
                return NotFound();
            }

            var company = await _industialDb.Companys.FirstOrDefaultAsync(mbox => mbox.Id == id);
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
    }
}
