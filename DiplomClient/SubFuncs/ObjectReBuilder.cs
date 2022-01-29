using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiplomClient;

namespace DiplomClient.SubFuncs
{
    class ObjectReBuilder
    {
        public static List<Doctor> ReBuildDoctorFromBytes(byte[] answer)
        {
            int lastByte = 0;
            int docId = 0, docExperience = 0, answerElementsCount = 0, constraintCount = 0;
            string docName = "", docSurname = "", docLogin = "";
            byte[] docFoto = new byte[0];
            List<Doctor> docs = new List<Doctor>();

            byte[] Bytes = new byte[0];

            //for (int i = lastByte; i < lastByte + 4; i++)       //All items(objects) count
            //{
            //    Bytes = Bytes.Append(answer[i]).ToArray();
            //}
            //Bytes = answer.Skip(startByte).Take(lastByte).ToArray();
            answerElementsCount = BitConverter.ToInt32(answer.Skip(lastByte).Take(4).ToArray(), 0);
            lastByte = lastByte + 4;

            for (int i = 0; i < answerElementsCount; i++)
            {
                //Bytes = new byte[0];
                //for (int j = lastByte; j < lastByte + 4; j++)       //Item's(object's) constraint count
                //{
                //    Bytes = Bytes.Append(answer[j]).ToArray();
                //}
                //Bytes = answer.Skip(startByte).Take(lastByte).ToArray();
                constraintCount = BitConverter.ToInt32(answer.Skip(lastByte).Take(4).ToArray(), 0);
                lastByte = lastByte + 4;

                for (int j = 0; j < constraintCount; j++)
                {
                    //Bytes = new byte[0];
                    //for (int ja = lastByte; ja < lastByte + 4; ja++)
                    //{
                    //    Bytes = Bytes.Append(answer[ja]).ToArray();
                    //}
                    int columnLength = BitConverter.ToInt32(answer.Skip(lastByte).Take(4).ToArray(), 0);
                    lastByte = lastByte + 4;
                    //Bytes = new byte[0];

                    //for (int jb = lastByte; jb < lastByte + columnLength; jb++)
                    //{
                    //    Bytes = Bytes.Append(answer[jb]).ToArray();
                    //}

                    if (j == 0)
                        docId = BitConverter.ToInt32(answer.Skip(lastByte).Take(columnLength).ToArray(), 0);
                    if (j == 1)
                        docName = Encoding.Unicode.GetString(answer.Skip(lastByte).Take(columnLength).ToArray());
                    if (j == 2)
                        docSurname = Encoding.Unicode.GetString(answer.Skip(lastByte).Take(columnLength).ToArray());
                    if (j == 3)
                        docLogin = Encoding.Unicode.GetString(answer.Skip(lastByte).Take(columnLength).ToArray());
                    if (j == 4)
                        docExperience = BitConverter.ToInt32(answer.Skip(lastByte).Take(columnLength).ToArray(), 0);
                    if (j == 5)
                        docFoto = answer.Skip(lastByte).Take(columnLength).ToArray();

                    lastByte = lastByte + columnLength;
                }
                docs.Add(new Doctor()
                {
                    Id = docId,
                    Name = docName,
                    Surname = docSurname,
                    Login = docLogin,
                    Experience = docExperience,
                    DocFoto = docFoto
                });
            }

            return docs;
        }
    }
}
