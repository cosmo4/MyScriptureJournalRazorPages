using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyScriptureJournalRazorPages.Data;
using MyScriptureJournalRazorPages.Models;

namespace MyScriptureJournalRazorPages.Pages.Entries
{
    public class CreateModel : PageModel
    {
        private readonly MyScriptureJournalRazorPages.Data.MyScriptureJournalRazorPagesContext _context;

        public CreateModel(MyScriptureJournalRazorPages.Data.MyScriptureJournalRazorPagesContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Entry Entry { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Entries.Add(Entry);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
