using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using lab12New;

namespace lab12New.Pages.Goods
{
    public class goodModel : PageModel
    {
        private readonly lab12New.Industrial _industrialDb;

        public goodModel(lab12New.Industrial industrialDb)
		{
            _industrialDb = industrialDb;
		}

        public IList<Good> Goods { get; set; } = default!;
        public async Task OnGetAsync()
        {
            if (_industrialDb.Goods != null)
            {
                Goods = await _industrialDb.Goods.ToListAsync();
            }
        }
    }
}
