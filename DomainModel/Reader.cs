using Library.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Reader
    {
        public int ReaderId { get; set; }

        public string ReaderFirstName { get; set; }

        public string ReaderLastName { get;set; }  

        public string Address { get; set; } 

        public string EmailAddress { get; set; }

        public bool Role { get; set; }
        public string PhoneNumber { get; set; }
    }
}
