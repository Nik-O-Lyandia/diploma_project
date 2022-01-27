using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomServer.SubFuncs
{
    class VisitBooking
    {
        public string VisitBookingFunc(string[] dataStringArray)
        {
            using (iToothServContext iToothServ = new iToothServContext())
            {
                string answerStr = "";

                var dataDoc = iToothServ.Doctors.AsQueryable();
                List<Doctor> docs = dataDoc.ToList();

                for (int i = 0; i < docs.Count(); i++)
                {
                    answerStr += docs[i].Id + "|" + docs[i].Name + "|" + docs[i].Surname + "|" + docs[i].Login + "$";
                }

                return answerStr;
            }
        }
    }
}
