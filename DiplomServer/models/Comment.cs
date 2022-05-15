using System;
using System.Collections.Generic;

#nullable disable

namespace DiplomServer.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Rating { get; set; }
        public string CommentText { get; set; }
        public int PacientId { get; set; }
        public int DoctorId { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual Pacient Pacient { get; set; }
    }
}
