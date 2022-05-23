using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomServer.Models;

namespace DiplomServer.SubFuncs
{
    class DocSchedule
    {
        public static string DocScheduleFunc(string[] dataStringArray)
        {
            using (iToothServContext iToothServ = new iToothServContext())
            {
                string answerStr = "";

                var dataRes = iToothServ.Reseptions.AsQueryable();
                var dataDoc = iToothServ.Doctors.AsQueryable();
                var dataPac = iToothServ.Pacients.AsQueryable();

                List<Reseption> reseptions = iToothServ.Reseptions.AsQueryable().ToList();
                List<string> docLogins = new List<string>();
                List<string> pacLogins = new List<string>();

                foreach (Reseption r in reseptions)
                {
                    docLogins.Add(dataDoc.Single(d => d.Id == r.DoctorId).Login);
                    pacLogins.Add(dataPac.Single(p => p.Id == r.PacientId).Login);
                }

                if (reseptions.Count() > 0)
                {
                    for (int i = 0; i < reseptions.Count(); i++)
                    {
                        if (docLogins[i] == dataStringArray[2])
                        {
                            if (dataStringArray[1].Equals("Пацієнт"))
                            {
                                answerStr += reseptions[i].Attendingdate.ToString() + "|" +
                                    //reseptions[i].Time + "|" +
                                    (9 + Convert.ToInt32(reseptions[i].Time) % 9) + ":00-" + (10 + Convert.ToInt32(reseptions[i].Time) % 9) + ":00" + "|" +
                                    dataDoc.Single(d => d.Login == docLogins[i]).Surname + " " + dataDoc.Single(d => d.Login == docLogins[i]).Name + "|" +
                                    dataDoc.Single(d => d.Login == docLogins[i]).Login + "$";
                            }
                            if (dataStringArray[1].Equals("Лікар"))
                            {
                                answerStr += reseptions[i].Attendingdate.ToString() + "|" +
                                    //reseptions[i].Time + "|" +
                                    (9 + Convert.ToInt32(reseptions[i].Time) % 9) + ":00-" + (10 + Convert.ToInt32(reseptions[i].Time) % 9) + ":00" + "|" +
                                    pacLogins[i] + "$";
                            }
                        }
                    }
                }
                else
                {
                    answerStr = "Немає запланованих візитів";
                }

                return answerStr;
            }
        }
    }
}
