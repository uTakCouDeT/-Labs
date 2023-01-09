using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using lab12New;


namespace lab12New.Pages.Companys
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
        public lab12New.Company company { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            _industialDb.Companys.Add(company);
            await _industialDb.SaveChangesAsync();

            return RedirectToPage("./Company");
        }
    }
}
