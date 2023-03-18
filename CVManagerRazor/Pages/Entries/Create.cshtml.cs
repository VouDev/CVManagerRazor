using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CVManagerRazor.Data;
using CVManagerRazor.Models;
using CVManagerRazor.FileUploadService;

namespace CVManagerRazor.Pages.Entries
{
    public class CreateModel : PageModel
    {
        private readonly CVManagerRazorContext _context;
        private readonly IFileUploadService _fileUploadService;

        [BindProperty]
        public Entry Entry { get; set; } = default!;

        public List<string> Degrees { get; set; } = new List<string>() { "Bachelor of Science(BSc)", "Master of Science(Msc)", "PhD Degree" };

        public CreateModel(CVManagerRazorContext context, IFileUploadService fileUploadService)
        {
            _context = context;
            _fileUploadService = fileUploadService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile? file)
        {
            if (!ModelState.IsValid || _context.Entry == null || Entry == null)
            {
                return Page();
            }

            if(file != null)
            {
                string ext = Path.GetExtension(file.FileName);
                if (ext == null || (ext != ".pdf" && ext != ".docx"))
                {
                    ModelState.AddModelError("Entry.File", "You can only upload pdf or word documents");
                    return Page();
                }
                Entry.File = await _fileUploadService.UploadFile(file);
            }

            _context.Entry.Add(Entry);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
