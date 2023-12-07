using Library.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Borrow
    {
        public int Id { get; set; }

        public virtual Reader Reader { get; set; }

        public virtual Edition Edition { get; set; }

        public DateTime BorrowStartDate { get; set; }

        public DateTime BorrowEndDate { get; set; }

        public bool IsReturned { get; set; }

        public virtual ICollection<ExtendedBorrow> ExtendedBorrows { get; set; } = new List<ExtendedBorrow>();
    }
}
