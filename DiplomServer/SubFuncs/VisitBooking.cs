using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiplomServer.SubFuncs
{
    class VisitBooking
    {
        public byte[] VisitBookingFunc(string[] dataStringArray)
        {
            using (iToothServContext iToothServ = new iToothServContext())
            {
                string answerStr = "";
                byte[] answer = new byte[0];

                var dataDoc = iToothServ.Doctors.AsQueryable();
                List<Doctor> docs = dataDoc.ToList();
                answer = answer.Concat(BitConverter.GetBytes(docs.Count())).ToArray();

                for (int i = 0; i < docs.Count(); i++)
                {
                    answer = answer.Concat(ObjectByteBuilder.BuildDoctorInBytes(docs[i])).ToArray();
                }

                return answer;
            }
        }
    }
}
