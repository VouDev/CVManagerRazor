using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CVManagerRazor.Data;
using CVManagerRazor.Models;

namespace CVManagerRazor.Pages.Entries
{
    public class DetailsModel : PageModel
    {
        private readonly CVManagerRazor.Data.CVManagerRazorContext _context;

        public DetailsModel(CVManagerRazor.Data.CVManagerRazorContext context)
        {
            _context = context;
        }

      public Entry Entry { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Entry == null)
            {
                return NotFound();
            }

            var entry = await _context.Entry.FirstOrDefaultAsync(m => m.Id == id);
            if (entry == null)
            {
                return NotFound();
            }
            else 
            {
                entry.File = Path.GetFileName(entry.File);
                Entry = entry;
            }
            return Page();
        }
    }
}
