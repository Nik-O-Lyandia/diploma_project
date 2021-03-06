using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomServer.Models;

namespace DiplomServer.SubFuncs
{
    class Comments
    {
        public static byte[] GetCommentsFunc(string[] dataStringArray)
        {
            using (iToothServContext iToothServ = new iToothServContext())
            {
                byte[] answer = new byte[0];
                var doc = iToothServ.Doctors.Single(d => d.Login == dataStringArray[1]);
                var coms = iToothServ.Comments.Where(c => c.DoctorId == doc.Id).ToList();

                answer = answer.Concat(BitConverter.GetBytes(coms.Count())).ToArray();

                for (int i = 0; i < coms.Count(); i++)
                {
                    var pac = iToothServ.Pacients.Single(p => p.Id == coms[i].PacientId);
                    answer = answer.Concat(ObjectByteBuilder.BuildCommentInBytes(coms[i], pac)).ToArray();
                }

                return answer;
            }
        }
    }
}
