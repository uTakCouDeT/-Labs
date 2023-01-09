using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace lab12New.Pages.Goods
{
    public class EditModel : PageModel
    {
        private readonly Industrial _industialDb;

        public EditModel(Industrial industialDb)
        {
            _industialDb = industialDb;
        }
        [BindProperty]
        public Good Good { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _industialDb.Goods == null)
            {
                return NotFound();
            }

            var good = await _industialDb.Goods.FirstOrDefaultAsync(m => m.Id == id);
            if (good == null)
            {
                return NotFound();
            }
            Good = good;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            
            _industialDb.Update(Good!);
            await _industialDb.SaveChangesAsync();

            return RedirectToPage("./good");
        }

        private bool GoodExists(int id)
        {
            return _industialDb.Goods.Any(e => e.Id == id);
        }
    }
}
