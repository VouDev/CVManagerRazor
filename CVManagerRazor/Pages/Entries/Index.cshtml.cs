using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CVManagerRazor.Data;
using CVManagerRazor.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CVManagerRazor.Pages.Entries
{
    public class IndexModel : PageModel
    {
        private readonly CVManagerRazorContext _context;

        public IndexModel(CVManagerRazorContext context)
        {
            _context = context;
        }

        public IList<Entry> Entry { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public async Task OnGetAsync()
        {
            var entries = _context.Entry.Select(e => e);
            if (!string.IsNullOrEmpty(SearchString))
            {
                entries = entries.Where(e => e.FirstName.Contains(SearchString) || e.LastName.Contains(SearchString));
            }

            Entry = await entries.ToListAsync();
            foreach (var entry in Entry) 
            {
                entry.File = Path.GetFileName(entry.File);
            }
        }
    }
}
