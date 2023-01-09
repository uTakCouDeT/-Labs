using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;



namespace lab12New.Pages.Recipients
{
    public class RecipientModel : PageModel
    {
        private readonly lab12New.Industrial _industrialDb;

        public RecipientModel(lab12New.Industrial industrialDb)
        {
            _industrialDb = industrialDb;
        }

        public IList<Recipient> Recipients { get; set; } = default!;
        public async Task OnGetAsync()
        {
            if (_industrialDb.Recipients != null)
            {
                Recipients = await _industrialDb.Recipients.ToListAsync();
            }
        }
    }
}
