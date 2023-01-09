using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using lab12New;

namespace lab12New.Pages.Companys
{
    public class CompanyModel : PageModel
    {
        private readonly lab12New.Industrial _industrialDb;

        public CompanyModel(lab12New.Industrial industrialDb)
        {
            _industrialDb = industrialDb;
        }

        public IList<lab12New.Company> Companies { get; set; } = default!;
        public async Task OnGetAsync()
        {
            if (_industrialDb.Goods != null)
            {
                Companies = await _industrialDb.Companys.ToListAsync();
            }
        }
    }
}
