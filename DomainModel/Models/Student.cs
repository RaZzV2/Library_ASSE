using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.models
{
    public class Student
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "First name is required!")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "First name must have at least 4 characters!")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "First name must not have special characters!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required!")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Last name must have at least 4 characters!")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Last name must not have special characters!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Age is required!")]
        [Range(7,99, ErrorMessage = "You must have at least 7 and at most 99 years to borrow a book!")]
        public int Age { get; set; }
    }
}
