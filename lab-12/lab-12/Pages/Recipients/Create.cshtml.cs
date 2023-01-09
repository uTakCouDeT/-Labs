using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lab12New.Pages.Recipients
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
        public Recipient recipient { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            _industialDb.Recipients.Add(recipient);
            await _industialDb.SaveChangesAsync();

            return RedirectToPage("./Recipient");
        }
    }
}
