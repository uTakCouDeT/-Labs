using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using lab12New;

namespace lab12New.Pages.Companys
{
    public class EditModel : PageModel
    {
        private readonly Industrial _industialDb;

        public EditModel(Industrial industialDb)
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
            _company = company;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            _industialDb.Update(_company!);
            await _industialDb.SaveChangesAsync();

            return RedirectToPage("./Company");
        }

        private bool GoodExists(int id)
        {
            return _industialDb.Goods.Any(e => e.Id == id);
        }
    }
}
