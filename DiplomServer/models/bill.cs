using System;
using System.Collections.Generic;

#nullable disable

namespace DiplomServer
{
    public partial class Bill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string Paied { get; set; }
        public int PacientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime IssueDate { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual Pacient Pacient { get; set; }
    }
}
