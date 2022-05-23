using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomServer.Models;

namespace DiplomServer.SubFuncs
{
    class Booking
    {
        public static bool BookingFunc(string[] dataStringArray)
        {
            using (iToothServContext iToothServ = new iToothServContext())
            {
                var dataPac = iToothServ.Pacients.AsQueryable();
                var dataDoc = iToothServ.Doctors.AsQueryable();

                iToothServ.Reseptions.Add(new Reseption()
                {
                    PacientId = dataPac.Single(p => p.Login == dataStringArray[1]).Id,
                    DoctorId = dataDoc.Single(p => p.Login == dataStringArray[2]).Id,
                    Attendingdate = Convert.ToDateTime(dataStringArray[3]),
                    Time = dataStringArray[4],
                    Status = 1
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
