using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CVManagerRazor.Data;
using CVManagerRazor.Models;
using CVManagerRazor.FileUploadService;

namespace CVManagerRazor.Pages.Entries
{
    public class EditModel : PageModel
    {
        private readonly CVManagerRazorContext _context;
        private readonly IFileUploadService _fileUploadService;

        [BindProperty]
        public Entry Entry { get; set; } = default!;

        public string? FileName { get; set; }
        public List<string> Degrees { get; set; } = new List<string>() { "Bachelor of Science(BSc)", "Master of Science(Msc)", "PhD Degree" };

        public EditModel(CVManagerRazorContext context, IFileUploadService fileUploadService)
        {
            _context = context;
            _fileUploadService = fileUploadService;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Entry == null)
            {
                return NotFound();
            }

            var entry =  await _context.Entry.FirstOrDefaultAsync(m => m.Id == id);
            if (entry == null)
            {
                return NotFound();
            }
            Entry = entry;
            FileName = Path.GetFileName(Entry.File);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile? newFile = null)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (newFile != null)
            {
                string ext = Path.GetExtension(newFile.FileName);
                if (ext == null || (ext != ".pdf" && ext != ".docx"))
                {
                    ModelState.AddModelError("Entry.File", "You can only upload pdf or word documents");
                    return Page();
                }
                Entry.File = await _fileUploadService.UploadFile(newFile);
            }

            _context.Attach(Entry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntryExists(Entry.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool EntryExists(int id)
        {
          return (_context.Entry?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
