using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace lab12New.Pages.Recipients
{
    public class DeleteModel : PageModel
    {
        private readonly Industrial _industialDb;

        public DeleteModel(Industrial industialDb)
        {
            _industialDb = industialDb;
        }

        [BindProperty]
        public Recipient recipient { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _industialDb.Recipients == null)
            {
                return NotFound();
            }

            var rec = await _industialDb.Recipients.FirstOrDefaultAsync(m => m.Id == id);

            if (rec == null)
            {
                return NotFound();
            }
            else
            {
                recipient = rec;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _industialDb.Recipients == null)
            {
                return NotFound();
            }
            var rec = await _industialDb.Recipients.FindAsync(id);

            if (rec != null)
            {
                recipient = rec;
                _industialDb.Recipients.Attach(rec);
                _industialDb.Recipients.Remove(rec);
                await _industialDb.SaveChangesAsync();
            }

            return RedirectToPage("./Recipient");
        }
    }
}
