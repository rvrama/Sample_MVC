using Microsoft.EntityFrameworkCore;
using Sample_MVC.Models;

namespace Sample_MVC.Models
{
    public class NotesContext : DbContext
    {
     //   public readonly DbContext context;
        public NotesContext(DbContextOptions<NotesContext> options) : base(options){
        }

        //public List<Notes> myNotes { get; set; }
        public DbSet<Sample_MVC.Models.Notes> Notes { get; set; } = default!;

    }

    public class Notes
    {
        public int NotesId { get; set; }

        public string NotesTitle { get; set; }

        public string NotesDescription { get; set; }

        public DateTime NotesCreatedAt { get; set; }
    }
}
