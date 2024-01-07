using System.ComponentModel.DataAnnotations;

namespace Library.models
{
    public class Edition
    {
        public int EditionId { get; set; }

        [Required(ErrorMessage = "Edition name is required!")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Edition name must have at least 4 characters!")]
        [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "Edition name must not have special characters!")]
        public string EditionName { get; set;}

        [Range(1900, 2100, ErrorMessage = "Invalid edition year. Must be between 1900 and 2024.")]
        public int EditionYear { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Number of pages must be at least 1.")]
        public int PagesNumber { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Number of borrowable books cannot be negative.")]
        public int BorrowableBooks { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Number of unborrowable books cannot be negative.")]
        public int UnBorrowableBooks { get; set; }

        [EnumDataType(typeof(Type), ErrorMessage = "Invalid book type.")]
        public Type BookType { get; set; }

        public enum Type
        {
            Hardcover,
            Paperback,
            Board,
            SpiralBound,
            RingBound
        }
    }
}
