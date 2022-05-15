using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiplomServer.Models;

namespace DiplomServer.SubFuncs
{
    class VisitBooking
    {
        public byte[] VisitBookingFunc(string[] dataStringArray)
        {
            using (iToothServContext iToothServ = new iToothServContext())
            {
                byte[] answer = new byte[0];

                var docs = iToothServ.Doctors.AsQueryable().ToList();

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
