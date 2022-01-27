using System;
using System.Collections.Generic;

#nullable disable

namespace DiplomServer
{
    public partial class Pacient
    {
        public Pacient()
        {
            Bills = new HashSet<Bill>();
            Comments = new HashSet<Comment>();
            Notes = new HashSet<Note>();
            PacientDoctors = new HashSet<PacientDoctor>();
            Reseptions = new HashSet<Reseption>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Passportnumber { get; set; }
        public string Passportseries { get; set; }
        public string Login { get; set; }
        public int PasswordHash { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<PacientDoctor> PacientDoctors { get; set; }
        public virtual ICollection<Reseption> Reseptions { get; set; }
    }
}
