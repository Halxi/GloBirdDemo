namespace GloBirdDemo2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("Customer")]
    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            CallNotes = new HashSet<CallNote>();
        }

        public int CustomerId { get; set; }

        [Remote("IsUserExists", "Customers", ErrorMessage = "User Name already in use")]
        [Required(ErrorMessage = "Please Enter Username.")]
        [RegularExpression("^[A-Za-z0-9]{5,20}$", ErrorMessage = "Invalid input, please try again.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please Enter First Name.")]
        [RegularExpression("^[ a-zA-Z]+$", ErrorMessage = "Invalid input, please try again.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter Last Name.")]
        [RegularExpression("^[ a-zA-Z]+$", ErrorMessage = "Invalid input, please try again.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please Enter Phone Number.")]
        [RegularExpression(@"^\({0,1}((0|\+61)(2|4|3|7|8)){0,1}\){0,1}(\ |-){0,1}[0-9]{2}(\ |-){0,1}[0-9]{2}(\ |-){0,1}[0-9]{1}(\ |-){0,1}[0-9]{3}$", ErrorMessage = "Please enter valid Australian phone number.")]
        public string PhoneNo { get; set; }
        [Required(ErrorMessage = "Please Enter Date Of Birth.")]
        public DateTime DOB { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CallNote> CallNotes { get; set; }
    }
}
