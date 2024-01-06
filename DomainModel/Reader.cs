using Library.models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    [PhoneOrEmailReq(ErrorMessage = "Either phone number or email address is required.")]
    public class Reader 
    {
        public int ReaderId { get; set; }


        [Required(ErrorMessage = "First name is required!")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "First name must have at least 4 characters!")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "First name must not have special characters!")]
        public string ReaderFirstName { get; set; }


        [Required(ErrorMessage = "Last name is required!")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Last name must have at least 4 characters!")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Last name must not have special characters!")]
        public string ReaderLastName { get;set; }


        [Required(ErrorMessage = "Address is required!")]
        public string Address { get; set; }


        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailAddress { get; set; }


        [Required(ErrorMessage = "Role is required!")]
        public bool Role { get; set; }

       
        [Phone(ErrorMessage = "Invalid Phone Number!")]
        public string PhoneNumber { get; set; }
    }
}
