namespace Library.models
{
    public class Edition
    {
        public int EditionId { get; set; }
        public string EditionName { get; set;}
        public int EditionYear { get; set; }
        public int PagesNumber { get; set; }
        public int BorrowableBooks { get; set; }
        public int UnBorrowableBooks { get; set; }
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
