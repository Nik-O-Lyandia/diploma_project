using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Linq;
using DiplomServer.SubFuncs;

namespace DiplomServer
{
    class Program
    {
        static void Main(string[] args)
        {
            const string ip = "127.0.0.1";
            const int port = 8080;

            bool boolAnswer;
            string ResultStr;
            byte[] ResultByteArr;
            string sendData = "";

            IPEndPoint tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            Socket tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                tcpSocket.Bind(tcpEndPoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: Exception in bind socket: " + ex);
            }
            tcpSocket.Listen(5);

            while (true)
            {
                Socket listener = tcpSocket.Accept();
                byte[] buffer = new byte[256];
                int size = 0;
                StringBuilder data = new StringBuilder();

                do
                {
                    size = listener.Receive(buffer);
                    data.Append(Encoding.Unicode.GetString(buffer, 0, size));
                }
                while (listener.Available > 0);

                string[] dataStringArray = data.ToString().Split("|");

                //---------------------Вхід в систему------------------------
                if (dataStringArray[0].Equals("LogIn"))
                {
                    boolAnswer = LogIn.LogInFunc(dataStringArray);

                    if (boolAnswer)
                    {
                        sendData = "True";
                    }
                    else
                    {
                        sendData = "False";
                    }
                }

                //---------------------Реєстрація в системі------------------------
                if (dataStringArray[0].Equals("SignIn"))
                {
                    ResultStr = SignIn.SignInFunc(dataStringArray);

                    if (ResultStr.Equals("True"))
                    {
                        sendData = "SignInTrue";
                    }
                    else
                    {
                        sendData = ResultStr;
                    }
                }

                //---------------------Перегляд ЛК------------------------
                if (dataStringArray[0].Equals("LookLK"))
                {
                    ResultStr = LookLK.LookLKFunc(dataStringArray);

                    if (!ResultStr.Equals(""))
                    {
                        sendData = ResultStr;
                    }
                    else
                    {
                        sendData = "LookLKError";
                    }
                }

                //---------------------Перегляд рахунків------------------------
                if (dataStringArray[0].Equals("MyBills"))
                {
                    ResultStr = MyBills.MyBillsFunc(dataStringArray);

                    if (!ResultStr.Equals(""))
                    {
                        sendData = ResultStr;
                    }
                    else
                    {
                        sendData = "MyBillsError";
                    }
                }

                //---------------------Перегляд візитів------------------------
                if (dataStringArray[0].Equals("MyVisits"))
                {
                    ResultStr = MyVisits.MyVisitsFunc(dataStringArray);

                    if (!ResultStr.Equals(""))
                    {
                        sendData = ResultStr;
                    }
                    else
                    {
                        sendData = "MyVisitsError";
                    }
                }

                //---------------------Сплата рахунку------------------------
                if (dataStringArray[0].Equals("PayBill"))
                {
                    boolAnswer = PayBill.PayBillFunc(dataStringArray);

                    if (boolAnswer)
                    {
                        sendData = "True";
                    }
                    else
                    {
                        sendData = "False";
                    }
                }

                //---------------------Запис на прийом------------------------
                if (dataStringArray[0].Equals("VisitBooking"))
                {
                    ResultByteArr = VisitBooking.VisitBookingFunc(dataStringArray);

                    if (ResultByteArr.Length != 0 || ResultByteArr != null)
                    {
                        listener.Send(ResultByteArr);
                    }
                    else
                    {
                        sendData = "VisitBookingError";
                    }
                }

                //---------------------Розклад лікаря------------------------
                if (dataStringArray[0].Equals("DocSchedule"))
                {
                    ResultStr = DocSchedule.DocScheduleFunc(dataStringArray);

                    if (!ResultStr.Equals(""))
                    {
                        sendData = ResultStr;
                    }
                    else
                    {
                        sendData = "DocScheduleError";
                    }
                }

                //---------------------Бронювання візиту------------------------
                if (dataStringArray[0].Equals("Booking"))
                {
                    boolAnswer = Booking.BookingFunc(dataStringArray);

                    if (boolAnswer)
                    {
                        sendData = "True";
                    }
                    else
                    {
                        sendData = "False";
                    }
                }

                //---------------------Закріплення пацієнта за доктором------------------------
                if (dataStringArray[0].Equals("FixPacient"))
                {
                    boolAnswer = FixPacient.FixPacientFunc(dataStringArray);

                    if (boolAnswer)
                    {
                        sendData = "True";
                    }
                    else
                    {
                        sendData = "False";
                    }
                }

                //---------------------Перегляд закріплених пацієнтів------------------------
                if (dataStringArray[0].Equals("MyPacients"))
                {
                    ResultStr = MyPacients.MyPacientsFunc(dataStringArray);

                    if (!ResultStr.Equals(""))
                    {
                        sendData = ResultStr;
                    }
                    else
                    {
                        sendData = "MyPacientsError";
                    }
                }

                //---------------------Редагування ЛК------------------------
                if (dataStringArray[0].Equals("EditLK"))
                {
                    boolAnswer = EditLK.EditLKFunc(dataStringArray);

                    if (boolAnswer)
                    {
                        sendData = "True";
                    }
                    else
                    {
                        sendData = "False";
                    }
                }

                //---------------------Додавання рахунку------------------------
                if (dataStringArray[0].Equals("AddBill"))
                {
                    boolAnswer = AddBill.AddBillFunc(dataStringArray);

                    if (boolAnswer)
                    {
                        sendData = "True";
                    }
                    else
                    {
                        sendData = "False";
                    }
                }

                //---------------------Завантаження коментарів------------------------
                if (dataStringArray[0].Equals("GetComments"))
                {
                    ResultByteArr = Comments.GetCommentsFunc(dataStringArray);

                    if (ResultByteArr.Length != 0 || ResultByteArr != null)
                    {
                        listener.Send(ResultByteArr);
                    }
                    else
                    {
                        sendData = "VisitBookingError";
                    }
                }

                //---------------------Відміна запису------------------------
                if (dataStringArray[0].Equals("CancelReseption"))
                {
                    boolAnswer = CancelReseption.CancelReseptionFunc(dataStringArray);

                    if (boolAnswer)
                    {
                        sendData = "True";
                    }
                    else
                    {
                        sendData = "False";
                    }
                }

                //---------------------Видалення запису------------------------
                if (dataStringArray[0].Equals("DeleteBill"))
                {
                    boolAnswer = DeleteBill.DeleteBillFunc(dataStringArray);

                    if (boolAnswer)
                    {
                        sendData = "True";
                    }
                    else
                    {
                        sendData = "False";
                    }
                }

                if (!dataStringArray[0].Equals("VisitBooking") && !dataStringArray[0].Equals("GetComments"))
                    listener.Send(Encoding.Unicode.GetBytes(sendData));

                listener.Shutdown(SocketShutdown.Both);
                listener.Close();
            }
        }
    }
}
