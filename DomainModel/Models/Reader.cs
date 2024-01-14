namespace DomainModel
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The <see cref="Reader"/> class represents a reader in the library system.
    /// </summary>
    [PhoneOrEmailReq(ErrorMessage = "Either phone number or email address is required.")]
    public class Reader
    {
        /// <summary>
        /// Gets or sets the unique identifier for the reader.
        /// </summary>
        public int ReaderId { get; set; }

        /// <summary>
        /// Gets or sets the first name of the reader.
        /// </summary>
        [Required(ErrorMessage = "First name is required!")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "First name must have at least 4 characters!")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "First name must not have special characters!")]
        public string ReaderFirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the reader.
        /// </summary>
        [Required(ErrorMessage = "Last name is required!")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Last name must have at least 4 characters!")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Last name must not have special characters!")]
        public string ReaderLastName { get;set; }

        /// <summary>
        /// Gets or sets the address of the reader.
        /// </summary>
        [Required(ErrorMessage = "Address is required!")]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the email address of the reader.
        /// </summary>
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a Reader is personal or not.
        /// </summary>
        [Required(ErrorMessage = "Role is required!")]
        public bool Role { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the reader.
        /// </summary>
        [Phone(ErrorMessage = "Invalid Phone Number!")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the list of borrow records associated with the reader.
        /// </summary>
        public List<Borrow> Borrows { get; set; }
    }
}
