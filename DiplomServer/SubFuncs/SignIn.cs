using System;
using System.Linq;
using System.Text;
using DiplomServer.Models;

namespace DiplomServer.SubFuncs
{
    class SignIn
    {
        public static string SignInFunc(string[] dataStringArray)
        {
            using (iToothServContext iToothServ = new iToothServContext())
            {
                bool LoginMatchesFound = false, PassportMatchesFound = false;
                string ResultMessage;

                //------------------------------------ Проверка логина на уникальность------------------------------------

                var dataPacient = iToothServ.Pacients.AsQueryable();
                var dataDoctor = iToothServ.Doctors.AsQueryable();

                for (int i = 0; i < dataPacient.Count(); i++)
                {
                    if (dataPacient.Single(a => a.Id == (i + 1)).Login == dataStringArray[2])
                    {
                        LoginMatchesFound = true;
                    }
                    if (dataPacient.Single(a => a.Id == (i + 1)).Passportseries == dataStringArray[4] && dataPacient.Single(a => a.Id == (i + 1)).Passportnumber.ToString() == dataStringArray[5])
                    {
                        PassportMatchesFound = true;
                    }
                }
                for (int i = 0; i < dataDoctor.Count(); i++)
                {
                    if (dataDoctor.Single(a => a.Id == (i + 1)).Login == dataStringArray[2])
                    {
                        LoginMatchesFound = true;
                    }
                    if (dataDoctor.Single(a => a.Id == (i + 1)).Passportseries == dataStringArray[4] && dataDoctor.Single(a => a.Id == (i + 1)).Passportnumber.ToString() == dataStringArray[5])
                    {
                        PassportMatchesFound = true;
                    }
                }


                //------------------------------------ Запись данных в базу------------------------------------
                if (LoginMatchesFound == true)
                    ResultMessage = "LoginMatchesFound";
                else
                    if (PassportMatchesFound == true)
                    ResultMessage = "PassportMatchesFound";
                else
                    ResultMessage = "AnotherError";

                if (LoginMatchesFound == false && PassportMatchesFound == false)
                {
                    if (dataStringArray[1] == "Пацієнт")
                    {
                        iToothServ.Pacients.Add(new Pacient()
                        {
                            Login = dataStringArray[2],
                            PasswordHash = BitConverter.ToInt32(Encoding.Unicode.GetBytes(dataStringArray[3])),
                            Passportseries = dataStringArray[4],
                            Passportnumber = Convert.ToInt32(dataStringArray[5]),
                            Name = dataStringArray[6],
                            Surname = dataStringArray[7]
                        });
                    }
                    if (dataStringArray[1] == "Лікар")
                    {
                        iToothServ.Doctors.Add(new Doctor()
                        {
                            Login = dataStringArray[2],
                            PasswordHash = BitConverter.ToInt32(Encoding.Unicode.GetBytes(dataStringArray[3])),
                            Passportseries = dataStringArray[4],
                            Passportnumber = Convert.ToInt32(dataStringArray[5]),
                            Name = dataStringArray[6],
                            Surname = dataStringArray[7]
                        });
                    }

                    try
                    {
                        iToothServ.SaveChanges();
                        ResultMessage = "True";
                    }
                    catch (Exception ex)
                    {
                        ResultMessage = "Error";
                        Console.WriteLine("Error: " + ex);
                    }
                }

                return ResultMessage;
            }
        }
    }
}
