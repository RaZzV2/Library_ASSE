using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class ExtendedBorrow
    {
        public int Id { get; set; } 
        public virtual Borrow Borrow { get; set; }

        public DateTime Date { get; set; } 
    }
}
