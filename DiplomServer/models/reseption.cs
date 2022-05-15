using System;
using System.Collections.Generic;

#nullable disable

namespace DiplomServer.Models
{
    public partial class Reseption
    {
        public int Id { get; set; }
        public DateTime? Attendingdate { get; set; }
        public string Time { get; set; }
        public int PacientId { get; set; }
        public int DoctorId { get; set; }
        public int? Status { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual Pacient Pacient { get; set; }
    }
}
