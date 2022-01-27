using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomServer.SubFuncs
{
    class LookLK
    {
        public string LookLKFunc(string[] dataStringArray)
        {
            using (iToothServContext iToothServ = new iToothServContext())
            {
                string answerStr = "";
                var dataNote = iToothServ.Notes.AsQueryable();
                var dataPac = iToothServ.Pacients.AsQueryable();
                var dataDoc = iToothServ.Doctors.AsQueryable();

                List<Note> notes = dataNote.ToList();

                if (dataStringArray[1].Equals("Пацієнт"))
                {
                    foreach (Note n in notes)
                    {
                        if (dataPac.Single(p => p.Id == n.PacientId).Login == dataStringArray[2])
                        {
                            answerStr += " -------\\ " + n.NoteDate + " /------- \n" + n.NoteText + "\n\n";
                        }
                    }
                }

                return answerStr;
            }
        }
    }
}
