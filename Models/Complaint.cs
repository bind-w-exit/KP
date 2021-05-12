using System;

namespace KP
{
    public class Complaint
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }


        public virtual User User { get; set; }

        public District District { get; set; }
    }
}
