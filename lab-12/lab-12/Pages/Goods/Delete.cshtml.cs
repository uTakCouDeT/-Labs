using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using lab12New;

namespace lab12New.Pages.Goods
{
    public class DeleteModel : PageModel
    {
        private readonly Industrial _industialDb;

        public DeleteModel(Industrial industialDb)
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
            else
            {
                Good = good;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _industialDb.Goods == null)
            {
                return NotFound();
            }
            var good = await _industialDb.Goods.FindAsync(id);

            if (good != null)
            {
                Good = good;
                _industialDb.Goods.Remove(Good);
                await _industialDb.SaveChangesAsync();
            }

            return RedirectToPage("./good");
        }
    }
}
