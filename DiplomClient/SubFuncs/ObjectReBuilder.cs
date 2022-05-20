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
            string docName = "", docSurname = "", docLogin = "", docRoom = "";
            double docRating = 0;
            byte[] docFoto = new byte[0];
            List<Doctor> docs = new List<Doctor>();

            answerElementsCount = BitConverter.ToInt32(answer.Skip(lastByte).Take(4).ToArray(), 0);
            lastByte = lastByte + 4;

            for (int i = 0; i < answerElementsCount; i++)
            {
                constraintCount = BitConverter.ToInt32(answer.Skip(lastByte).Take(4).ToArray(), 0);
                lastByte = lastByte + 4;

                for (int j = 0; j < constraintCount; j++)
                {
                    int columnLength = BitConverter.ToInt32(answer.Skip(lastByte).Take(4).ToArray(), 0);
                    lastByte = lastByte + 4;

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
                        docRoom = Encoding.Unicode.GetString(answer.Skip(lastByte).Take(columnLength).ToArray());
                    if (j == 6)
                        docRating = BitConverter.ToDouble(answer.Skip(lastByte).Take(columnLength).ToArray(), 0);
                    if (j == 7)
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
                    Room = docRoom,
                    Rating = docRating,
                    DocFoto = docFoto
                });
            }

            return docs;
        }
    }
}
