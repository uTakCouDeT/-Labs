using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using lab12New;

namespace lab12New.Pages.Goods
{
    public class CreateModel : PageModel
    {
        private readonly Industrial _industialDb;

        public CreateModel(Industrial industialDb)
        {
            _industialDb = industialDb;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Good good { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            
            _industialDb.Goods.Add(good);
            await _industialDb.SaveChangesAsync();

            return RedirectToPage("./good");
        }
    }
}
