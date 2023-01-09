using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace lab12New.Pages.Recipients
{
    public class EditModel : PageModel
    {
        private readonly Industrial _industialDb;

        public EditModel(Industrial industialDb)
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
            recipient = rec;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            _industialDb.Update(recipient!);
            await _industialDb.SaveChangesAsync();

            return RedirectToPage("./Recipient");
        }

        private bool GoodExists(int id)
        {
            return _industialDb.Goods.Any(e => e.Id == id);
        }
    }
}
