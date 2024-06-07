using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyScriptureJournalRazorPages.Data;
using MyScriptureJournalRazorPages.Models;

namespace MyScriptureJournalRazorPages.Pages.Entries
{
    public class DetailsModel : PageModel
    {
        private readonly MyScriptureJournalRazorPages.Data.MyScriptureJournalRazorPagesContext _context;

        public DetailsModel(MyScriptureJournalRazorPages.Data.MyScriptureJournalRazorPagesContext context)
        {
            _context = context;
        }

        public Entry Entry { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entry = await _context.Entries.FirstOrDefaultAsync(m => m.Id == id);
            if (entry == null)
            {
                return NotFound();
            }
            else
            {
                Entry = entry;
            }
            return Page();
        }
    }
}
