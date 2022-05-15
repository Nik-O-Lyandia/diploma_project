using System;
using System.Collections.Generic;

#nullable disable

namespace DiplomServer.Models
{
    public partial class Note
    {
        public int Id { get; set; }
        public DateTime NoteDate { get; set; }
        public string NoteText { get; set; }
        public int PacientId { get; set; }

        public virtual Pacient Pacient { get; set; }
    }
}
