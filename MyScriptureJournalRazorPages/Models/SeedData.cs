using Microsoft.EntityFrameworkCore;
using MyScriptureJournalRazorPages.Data;

namespace MyScriptureJournalRazorPages.Models;

public class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MyScriptureJournalRazorPagesContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MyScriptureJournalRazorPagesContext>>()))
        {
            if (context == null || context.Entries == null)
            {
                throw new ArgumentNullException("Null RazorPagesEntryContext");
            }

            // Look for any Entrys.
            if (context.Entries.Any())
            {
                return;   // DB has been seeded
            }

            context.Entries.AddRange(
                new Entry
                {
                    ScriptureReference = "1 Nephi 1:1",
                    EntryDate = DateTime.Parse("2024-2-12"),
                    Notes = "First verse in the Book of Mormon."
                },

                new Entry
                {
                    ScriptureReference = "Genesis 1:1",
                    EntryDate = DateTime.Parse("2020-4-16"),
                    Notes = "First verse in the Old Testament."
                },

                new Entry
                {
                    ScriptureReference = "Matthew 1:1",
                    EntryDate = DateTime.Parse("2023-2-21"),
                    Notes = "First verse in the New Testament."
                },

                new Entry
                {
                    ScriptureReference = "D&C 1:1",
                    EntryDate = DateTime.Parse("2022-8-6"),
                    Notes = "First verse in the Doctrine and Covenants."
                }
            );
            context.SaveChanges();
        }
    }
}
