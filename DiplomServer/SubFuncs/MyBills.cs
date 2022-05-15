using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomServer.Models;

namespace DiplomServer.SubFuncs
{
    class MyBills
    {
        public string MyBillsFunc(string[] dataStringArray)
        {
            using (iToothServContext iToothServ = new iToothServContext())
            {
                string answerStr = "";

                var dataBill = iToothServ.Bills.AsQueryable();
                var dataDoc = iToothServ.Doctors.AsQueryable();
                var dataPac = iToothServ.Pacients.AsQueryable();

                List<Bill> bills = dataBill.ToList();
                List<string> docLogins = new List<string>();
                List<string> pacLogins = new List<string>();
                foreach (Bill b in bills)
                {
                    docLogins.Add(dataDoc.Single(d => d.Id == b.DoctorId).Login);
                    pacLogins.Add(dataPac.Single(p => p.Id == b.PacientId).Login);
                }

                if (bills.Count() > 0)
                {
                    for (int i = 0; i < bills.Count(); i++)
                    {
                        if (docLogins[i] == dataStringArray[2] && bills[i].Paied == "0")
                        {
                            if (dataStringArray[1].Equals("Пацієнт"))
                            {
                                answerStr += bills[i].IssueDate.ToString() + "|" + bills[i].Name + "|" + bills[i].Cost.ToString() + "|" +
                                    dataDoc.Single(d => d.Id == bills[i].DoctorId).Surname + " " + dataDoc.Single(d => d.Id == bills[i].DoctorId).Name + "|" +
                                    docLogins[i] + "$";
                            }
                            if (dataStringArray[1].Equals("Лікар"))
                            {
                                answerStr += bills[i].IssueDate.ToString() + "|" + bills[i].Name + "|" + bills[i].Cost.ToString() + "|" +
                                    dataPac.Single(d => d.Id == bills[i].PacientId).Surname + " " + dataPac.Single(d => d.Id == bills[i].PacientId).Name + "|" +
                                    docLogins[i] + "$";
                            }
                        }

                    }
                }
                else
                {
                    answerStr = "Немає рахунків";
                }

                return answerStr;
            }
        }
    }
}
