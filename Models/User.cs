using System;
using System.Collections.Generic;

namespace KP 
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNum { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public virtual ICollection<House> Houses { get; set; }

        public virtual ICollection<Complaint> Appeals { get; set; }
    }
}
