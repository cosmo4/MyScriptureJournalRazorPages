using System.ComponentModel.DataAnnotations;

namespace MyScriptureJournalRazorPages.Models
{
    public class Entry
    {
        public int Id { get; set; }
        public string? ScriptureReference { get; set; }
        [DataType(DataType.Date)]
        public DateTime EntryDate { get; set; }
        public string? Notes { get; set; }
    }
}
