using Microsoft.EntityFrameworkCore;
using CVManagerRazor.Data;

namespace CVManagerRazor.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new CVManagerRazorContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<CVManagerRazorContext>>()))
        {
            if (context == null || context.Entry == null)
            {
                throw new ArgumentNullException("Null CVManagerRazorContext");
            }

            // Look for any movies.
            if (context.Entry.Any())
            {
                return;   // DB has been seeded
            }

            context.Entry.AddRange(
                new Entry
                {
                    FirstName = "test",
                    LastName = "test",
                    Email = "test@test.org",
                    Mobile = "6999999999",
                    Degree = "test"
                }
            );
            context.SaveChanges();
        }
    }
}