using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace lab12New.Pages.Goods
{
    public class DetailsModel : PageModel
    {
        private readonly Industrial _industialDb;

        public DetailsModel(Industrial industialDb)
        {
            _industialDb = industialDb;
        }

        public Good Good { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _industialDb.Goods == null)
			{
                return NotFound();
			}

            var good = await _industialDb.Goods.FirstOrDefaultAsync(mbox => mbox.Id == id);
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
    }
}
