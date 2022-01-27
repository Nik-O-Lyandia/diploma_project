using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomServer.SubFuncs
{
    class PayBill
    {
        public bool PayBillFunc(string[] dataStringArray)
        {
            using (iToothServContext iToothServ = new iToothServContext())
            {
                var query = (from b in iToothServ.Bills where b.Name == dataStringArray[3] select b).First();

                query.Paied = "1";
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
