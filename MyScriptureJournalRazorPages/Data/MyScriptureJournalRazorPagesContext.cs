using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyScriptureJournalRazorPages.Models;

namespace MyScriptureJournalRazorPages.Data
{
    public class MyScriptureJournalRazorPagesContext : DbContext
    {
        public MyScriptureJournalRazorPagesContext (DbContextOptions<MyScriptureJournalRazorPagesContext> options)
            : base(options)
        {
        }

        public DbSet<MyScriptureJournalRazorPages.Models.Entry> Entries { get; set; } = default!;
    }
}
