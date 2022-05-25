using DiplomServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiplomServer.SubFuncs
{
    class DeleteBill
    {
        public static bool DeleteBillFunc(string[] dataStringArray)
        {
            using (iToothServContext iToothServ = new iToothServContext())
            {
                int id = Convert.ToInt32(dataStringArray[1]);
                Bill bill = iToothServ.Bills.Single(b => b.Id == id);

                try
                {
                    iToothServ.Bills.Remove(bill);

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
