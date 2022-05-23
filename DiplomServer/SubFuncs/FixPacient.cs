using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomServer.Models;

namespace DiplomServer.SubFuncs
{
    class FixPacient
    {
        public static bool FixPacientFunc(string[] dataStringArray)
        {
            using (iToothServContext iToothServ = new iToothServContext())
            {
                var dataPac = iToothServ.Pacients.AsQueryable();
                var dataDoc = iToothServ.Doctors.AsQueryable();
                var dataPD = iToothServ.PacientDoctors.AsQueryable();
                bool addAllowed = true;
                bool addComleted = false;
                //Console.WriteLine(dataStringArray[2]);
                int docId = dataDoc.Single(p => p.Login == dataStringArray[1]).Id;
                int pacId = dataPac.Single(p => p.Login == dataStringArray[2]).Id;

                for (int i = 0; i < dataPD.Count(); i++)
                {
                    if (dataPD.Any(pd => pd.DoctorId == docId && pd.PacientId == pacId))
                    {
                        addAllowed = false;
                    }
                }

                if (addAllowed)
                {
                    iToothServ.PacientDoctors.Add(new PacientDoctor()
                    {
                        DoctorId = dataDoc.Single(p => p.Login == dataStringArray[1]).Id,
                        PacientId = dataPac.Single(p => p.Login == dataStringArray[2]).Id,
                    });
                    addComleted = true;
                }

                try
                {
                    if (addComleted)
                        iToothServ.SaveChanges();
                    else
                        return false;

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
