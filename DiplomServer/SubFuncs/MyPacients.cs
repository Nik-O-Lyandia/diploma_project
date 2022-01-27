using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomServer.SubFuncs
{
    class MyPacients
    {
        public string MyPacientsFunc(string[] dataStringArray)
        {
            using (iToothServContext iToothServ = new iToothServContext())
            {
                string answerStr = "";

                var dataPac = iToothServ.Pacients.AsQueryable();
                var dataPD = iToothServ.PacientDoctors.AsQueryable();

                var pacLogs = (from d in iToothServ.Doctors
                               join pd in iToothServ.PacientDoctors on d.Id equals pd.DoctorId
                               join p in iToothServ.Pacients on pd.PacientId equals p.Id
                               where d.Login == dataStringArray[1]
                               select p.Login).ToList();

                if (pacLogs.Count() > 0)
                {
                    for (int i = 0; i < pacLogs.Count(); i++)
                    {
                        answerStr += dataPac.Single(p => p.Login == pacLogs[i]).Name + "|" + dataPac.Single(p => p.Login == pacLogs[i]).Surname + "|" + pacLogs[i] + "$";
                    }
                }
                else
                {
                    answerStr = "Немає закріплених пацієнтів";
                }

                return answerStr;
            }
        }
    }
}
