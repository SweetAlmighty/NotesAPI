using Microsoft.EntityFrameworkCore;

namespace NotesAPI.Models
{
    public class NoteContext : DbContext
    {
        public DbSet<NoteItem> Notes { get; set; }

        public NoteContext(DbContextOptions<NoteContext> options)
            : base(options)
        {
        }
    }
}