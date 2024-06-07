using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyScriptureJournalRazorPages.Data;
using MyScriptureJournalRazorPages.Models;

namespace MyScriptureJournalRazorPages.Pages.Entries
{
    public class IndexModel : PageModel
    {
        private readonly MyScriptureJournalRazorPagesContext _context;

        public IndexModel(MyScriptureJournalRazorPagesContext context)
        {
            _context = context;
        }

        public IList<Entry> Entry { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public IList<string> Notes { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? NoteKeywordSearch { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortOrder { get; set; }

        public async Task OnGetAsync()
        {
            var entries = from e in _context.Entries select e;
            var note = from e in _context.Entries select e.Notes;

            if (!string.IsNullOrEmpty(SearchString))
            {
                entries = entries.Where(s => s.ScriptureReference.Contains(SearchString));
            }
            if (!string.IsNullOrEmpty(NoteKeywordSearch))
            {
                entries = entries.Where(s => s.Notes.Contains(NoteKeywordSearch));
            }

            // Fetch the data from the database
            var entriesList = await entries.ToListAsync();

            // Sorting logic (in-memory)
            switch (SortOrder)
            {
                case "Date":
                    entriesList = entriesList.OrderBy(e => e.EntryDate).ToList();
                    break;
                case "Book":
                    entriesList = entriesList.OrderBy(e => NormalizeBookName(e.ScriptureReference)).ThenBy(e => e.ScriptureReference).ToList();
                    break;
                default:
                    entriesList = entriesList.OrderBy(e => e.Id).ToList(); // Default sorting
                    break;
            }

            Notes = await note.ToListAsync();
            Entry = entriesList;
        }

        private string NormalizeBookName(string scriptureReference)
        {
            // Regex pattern to match the book name and numeric prefix (if present)
            var match = Regex.Match(scriptureReference, @"^(\d\s*)?([^\d:]+)").Groups;
            string numericPrefix = match[1].Value.Trim();
            string bookName = match[2].Value.Trim();

            // Normalize the book name by padding numeric prefixes with leading zeros
            string normalizedBookName = (numericPrefix != "" ? numericPrefix.PadLeft(2, '0') + " " : "") + bookName;

            return normalizedBookName;
        }
    }
}
