using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace lab12New.Pages.Recipients
{
    public class DetailsModel : PageModel
    {
        private readonly Industrial _industialDb;

        public DetailsModel(Industrial industialDb)
        {
            _industialDb = industialDb;
        }

        public Recipient recipient { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _industialDb.Recipients == null)
            {
                return NotFound();
            }

            var rec = await _industialDb.Recipients.FirstOrDefaultAsync(mbox => mbox.Id == id);
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
    }
}
