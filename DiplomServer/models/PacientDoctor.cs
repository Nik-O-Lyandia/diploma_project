using System;
using System.Collections.Generic;

#nullable disable

namespace DiplomServer
{
    public partial class PacientDoctor
    {
        public int PacientDoctorId { get; set; }
        public int PacientId { get; set; }
        public int DoctorId { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual Pacient Pacient { get; set; }
    }
}
