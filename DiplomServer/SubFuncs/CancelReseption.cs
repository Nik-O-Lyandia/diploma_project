using DiplomServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomServer.SubFuncs
{
    class CancelReseption
    {
        public static bool CancelReseptionFunc(string[] dataStringArray)
        {
            using (iToothServContext iToothServ = new iToothServContext())
            {
                int id = Convert.ToInt32(dataStringArray[1]);
                Reseption res = iToothServ.Reseptions.Single(r => r.Id == id);

                try
                {
                    res.Status = 4;

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
