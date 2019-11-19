namespace GloBirdDemo2.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class GloBirdModel : DbContext
    {
        public GloBirdModel()
            : base("name=GloBirdModel")
        {
        }

        public virtual DbSet<CallNote> CallNotes { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CallNote>()
                .HasMany(e => e.CallNotes)
                .WithOptional(e => e.ChildCallNote)
                .HasForeignKey(e => e.ParentCallNoteId);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.CallNotes)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);
        }
    }
}
