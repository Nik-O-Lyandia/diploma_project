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
            int docId = 0, docExperience = 0, answerElementsCount, constraintCount;
            string docName = "", docSurname = "", docLogin = "", docRoom = "";
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
                    DocFoto = docFoto
                });
            }

            return docs;
        }

        public static List<Comment> ReBuildCommentFromBytes(byte[] answer)
        {
            int lastByte = 0;
            int comId = 0, comRating = 0, answerElementsCount, constraintCount;
            long comDate = 0;
            string comText = "", pacPIB = "";

            List<Comment> coms = new List<Comment>();

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
                        comId = BitConverter.ToInt32(answer.Skip(lastByte).Take(columnLength).ToArray(), 0);
                    if (j == 1)
                        comDate = BitConverter.ToInt64(answer.Skip(lastByte).Take(columnLength).ToArray(), 0);
                    if (j == 2)
                        comRating = BitConverter.ToInt32(answer.Skip(lastByte).Take(columnLength).ToArray(), 0);
                    if (j == 3)
                        comText = Encoding.Unicode.GetString(answer.Skip(lastByte).Take(columnLength).ToArray());
                    if (j == 4)
                        pacPIB = Encoding.Unicode.GetString(answer.Skip(lastByte).Take(columnLength).ToArray());

                    lastByte = lastByte + columnLength;
                }
                coms.Add(new Comment()
                {
                    Id = comId,
                    Date = DateTime.FromBinary(comDate).ToString(),
                    Rating = comRating,
                    CommentText = comText,
                    PacientPIB = pacPIB,
                });
            }

            return coms;
        }
    }
}
