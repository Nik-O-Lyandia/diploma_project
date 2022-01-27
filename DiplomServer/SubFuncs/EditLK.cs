using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomServer.SubFuncs
{
    class EditLK
    {
        public bool EditLKFunc(string[] dataStringArray)
        {
            using (iToothServContext iToothServ = new iToothServContext())
            {
                iToothServ.Notes.Add(new Note()
                {
                    NoteDate = DateTime.Now,
                    NoteText = dataStringArray[2],
                    PacientId = (from p in iToothServ.Pacients where p.Login == dataStringArray[1] select p.Id).First()
                });

                try
                {
                    iToothServ.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex);
                    return false;
                }

            }
        }
    }
}
