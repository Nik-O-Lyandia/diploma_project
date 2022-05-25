using DiplomServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomServer.SubFuncs
{
    class PacVisited
    {
        public static bool PacVisitedFunc(string[] dataStringArray)
        {
            using (iToothServContext iToothServ = new iToothServContext())
            {
                Reseption res = iToothServ.Reseptions.Single(r => r.Id == Convert.ToInt32(dataStringArray[1]));

                try
                {
                    res.Status = 2;

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
