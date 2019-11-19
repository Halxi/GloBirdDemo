namespace GloBirdDemo2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CallNote")]
    public partial class CallNote
    {
        private CallNote childCallNote;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CallNote()
        {
            CallNotes = new HashSet<CallNote>();
        }

        public int CallNoteId { get; set; }

        [Required(ErrorMessage = "Please Enter Something.")]
        public string Text { get; set; }

        public DateTime DateTime { get; set; }

        [Required(ErrorMessage = "Please Choose a Customer.")]
        public int CustomerId { get; set; }

        public int? ParentCallNoteId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CallNote> CallNotes { get; set; }

        public virtual CallNote ChildCallNote { get => childCallNote; set => childCallNote = value; }

        public virtual Customer Customer { get; set; }
    }
}
