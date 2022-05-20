using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomServer.Models;

namespace DiplomServer.SubFuncs
{
    class MyVisits
    {
        public string MyVisitsFunc(string[] dataStringArray)
        {
            using (iToothServContext iToothServ = new iToothServContext())
            {
                string answerStr = "";

                var dataVisits = iToothServ.Reseptions.AsQueryable();
                var dataDoc = iToothServ.Doctors.AsQueryable();
                var dataPac = iToothServ.Pacients.AsQueryable();

                List<Reseption> reseptions = dataVisits.ToList();
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
                        if (pacLogins[i] == dataStringArray[2])
                        {
                            int docId = reseptions[i].DoctorId;
                            string status = "Невідомо";

                            if (reseptions[i].Status == 1) status = "Активний";
                            if (reseptions[i].Status == 2) status = "Завершений";
                            if (reseptions[i].Status == 3) status = "Не з'явився";
                            if (reseptions[i].Status == 4) status = "Відмінений";

                            answerStr += reseptions[i].Attendingdate.ToString() + "|" + 
                                (9 + Convert.ToInt32(reseptions[i].Time) % 9) + ":00-" + (10 + Convert.ToInt32(reseptions[i].Time) % 9) + ":00" + "|" + 
                                dataDoc.Single(d => d.Id == docId).Surname + " " + dataDoc.Single(d => d.Id == docId).Name + "|" +
                                status + "$";
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
