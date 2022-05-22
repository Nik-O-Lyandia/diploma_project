using System;
using System.Collections.Generic;

namespace DiplomClient
{
    public partial class Comment
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public int Rating { get; set; }
        public string CommentText { get; set; }
        public int PacientId { get; set; }
        public int DoctorId { get; set; }
        public string PacientPIB { get; set; }
    }
}
