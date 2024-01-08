using System;

namespace DomainModel
{
    public class ExtendedBorrow
    {
        public int Id { get; set; } 
        public virtual Borrow Borrow { get; set; }

        public DateTime Date { get; set; } 
    }
}
