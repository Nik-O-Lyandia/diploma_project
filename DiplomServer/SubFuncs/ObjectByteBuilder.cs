using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomServer.Models;
/*
 * This class contains builder functions for every object to send them via WebSockets.
 * Every sending object is a byte array which has such structure:
 *      - [4 bytes] - Amount of columns
 */
namespace DiplomServer.SubFuncs
{
    class ObjectByteBuilder
    {
        public static byte[] BuildDoctorInBytes(Doctor doc)
        {
            byte[] columnCount = BitConverter.GetBytes(7);
            byte[] docId = ToByteArrConverter.ToByteArr(doc.Id);
            byte[] docName = ToByteArrConverter.ToByteArr(doc.Name);
            byte[] docSurname = ToByteArrConverter.ToByteArr(doc.Surname);
            byte[] docLogin = ToByteArrConverter.ToByteArr(doc.Login);
            byte[] docRoom = ToByteArrConverter.ToByteArr(doc.Room);
            byte[] docExperience = ToByteArrConverter.ToByteArr(doc.Experience);
            byte[] docFoto = ToByteArrConverter.ToByteArr(doc.DocFoto);

            return columnCount.Concat(docId).Concat(docName).Concat(docSurname).Concat(docLogin).Concat(docExperience).Concat(docRoom).Concat(docFoto).ToArray();
        }

        public static byte[] BuildCommentInBytes(Comment com, Pacient pac)
        {
            byte[] columnCount = BitConverter.GetBytes(5);
            byte[] comId = ToByteArrConverter.ToByteArr(com.Id);
            byte[] comDate = ToByteArrConverter.ToByteArr(com.Date);
            byte[] comRating = ToByteArrConverter.ToByteArr(com.Rating);
            byte[] comText = ToByteArrConverter.ToByteArr(com.CommentText);
            byte[] pacPIB = ToByteArrConverter.ToByteArr(pac.Name+" "+pac.Surname);

            return columnCount.Concat(comId).Concat(comDate).Concat(comRating).Concat(comText).Concat(pacPIB).ToArray();
        }

        //    public static byte[] BuildPacientInBytes(Pacient pac)
        //    {
        //        byte[] docId = ToByteArrConverter.ToByteArr(doc.Id);
        //        byte[] docName = ToByteArrConverter.ToByteArr(doc.Name);
        //        byte[] docSurname = ToByteArrConverter.ToByteArr(doc.Surname);
        //        byte[] docLogin = ToByteArrConverter.ToByteArr(doc.Login);
        //        byte[] docExperience = ToByteArrConverter.ToByteArr(doc.Experience);
        //        byte[] docFoto = ToByteArrConverter.ToByteArr(doc.DocFoto);

        //        return docId.Concat(docName).Concat(docSurname).Concat(docLogin).Concat(docExperience).Concat(docFoto).ToArray();
        //    }

        //    public static byte[] BuildDoctorInBytes(Doctor doc)
        //    {
        //        byte[] docId = ToByteArrConverter.ToByteArr(doc.Id);
        //        byte[] docName = ToByteArrConverter.ToByteArr(doc.Name);
        //        byte[] docSurname = ToByteArrConverter.ToByteArr(doc.Surname);
        //        byte[] docLogin = ToByteArrConverter.ToByteArr(doc.Login);
        //        byte[] docExperience = ToByteArrConverter.ToByteArr(doc.Experience);
        //        byte[] docFoto = ToByteArrConverter.ToByteArr(doc.DocFoto);

        //        return docId.Concat(docName).Concat(docSurname).Concat(docLogin).Concat(docExperience).Concat(docFoto).ToArray();
        //    }

        //    public static byte[] BuildDoctorInBytes(Doctor doc)
        //    {
        //        byte[] docId = ToByteArrConverter.ToByteArr(doc.Id);
        //        byte[] docName = ToByteArrConverter.ToByteArr(doc.Name);
        //        byte[] docSurname = ToByteArrConverter.ToByteArr(doc.Surname);
        //        byte[] docLogin = ToByteArrConverter.ToByteArr(doc.Login);
        //        byte[] docExperience = ToByteArrConverter.ToByteArr(doc.Experience);
        //        byte[] docFoto = ToByteArrConverter.ToByteArr(doc.DocFoto);

        //        return docId.Concat(docName).Concat(docSurname).Concat(docLogin).Concat(docExperience).Concat(docFoto).ToArray();
        //    }
    }
}
