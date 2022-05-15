using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomServer.Models;

namespace DiplomServer.SubFuncs
{
    class AddBill
    {
        public bool AddBillFunc(string[] dataStringArray)
        {
            using (iToothServContext iToothServ = new iToothServContext())
            {
                var dataPac = iToothServ.Pacients.AsQueryable();
                var dataDoc = iToothServ.Doctors.AsQueryable();

                Console.WriteLine(Convert.ToDecimal(dataStringArray[4]));

                iToothServ.Bills.Add(new Bill()
                {
                    PacientId = dataPac.Single(p => p.Login == dataStringArray[1]).Id,
                    DoctorId = dataDoc.Single(p => p.Login == dataStringArray[2]).Id,
                    Name = dataStringArray[3],
                    Cost = Convert.ToDecimal(dataStringArray[4]),
                    Paied = "0",
                    IssueDate = DateTime.Now
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
