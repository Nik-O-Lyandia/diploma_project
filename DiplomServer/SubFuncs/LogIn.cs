using System;
using System.Linq;
using System.Text;

namespace DiplomServer.SubFuncs
{
    class LogIn
    {
        public bool LogInFunc(string[] dataStringArray)
        {
            using (iToothServContext iToothServ = new iToothServContext())
            {
                bool EnterAllowed = false;

                var dataPacient = iToothServ.Pacients.AsQueryable();
                var dataDoctor = iToothServ.Doctors.AsQueryable();

                if (dataStringArray[1].Equals("Пацієнт"))
                {
                    for (int i = 0; i < dataPacient.Count(); i++)
                    {
                        if (dataPacient.Single(a => a.Id == (i + 1)).Login == dataStringArray[2] &&
                            dataPacient.Single(a => a.Id == (i + 1)).PasswordHash == BitConverter.ToInt32(Encoding.Unicode.GetBytes(dataStringArray[3])))
                        {
                            EnterAllowed = true;
                        }
                    }
                }
                if (dataStringArray[1].Equals("Лікар"))
                {
                    for (int i = 0; i < dataDoctor.Count(); i++)
                    {
                        if (dataDoctor.Single(a => a.Id == (i + 1)).Login == dataStringArray[2] &&
                            dataDoctor.Single(a => a.Id == (i + 1)).PasswordHash == BitConverter.ToInt32(Encoding.Unicode.GetBytes(dataStringArray[3])))
                        {
                            EnterAllowed = true;
                        }
                    }
                }

                if (EnterAllowed)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
