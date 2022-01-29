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
        static SignIn signIn = new SignIn();
        static LogIn logIn = new LogIn();
        static LookLK lookLK = new LookLK();
        static MyBills myBills = new MyBills(); 
        static MyVisits myVisits = new MyVisits();
        static PayBill payBill = new PayBill(); 
        static VisitBooking visitBooking = new VisitBooking();
        static DocSchedule docSchedule = new DocSchedule();
        static Booking booking = new Booking();
        static FixPacient fixPacient = new FixPacient(); 
        static MyPacients myPacients = new MyPacients();
        static EditLK editLK = new EditLK();
        static AddBill addBill = new AddBill();

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
                    boolAnswer = logIn.LogInFunc(dataStringArray);

                    if (boolAnswer)
                    {
                        sendData = "LogInTrue";
                    }
                    else
                    {
                        sendData = "LogInFalse";
                    }
                }

                //---------------------Реєстрація в системі------------------------
                if (dataStringArray[0].Equals("SignIn"))
                {
                    ResultStr = signIn.SignInFunc(dataStringArray);

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
                    ResultStr = lookLK.LookLKFunc(dataStringArray);

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
                    ResultStr = myBills.MyBillsFunc(dataStringArray);

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
                    ResultStr = myVisits.MyVisitsFunc(dataStringArray);

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
                    boolAnswer = payBill.PayBillFunc(dataStringArray);

                    if (boolAnswer)
                    {
                        sendData = "PayBillTrue";
                    }
                    else
                    {
                        sendData = "PayBillFalse";
                    }
                }

                //---------------------Запис на прийом------------------------
                if (dataStringArray[0].Equals("VisitBooking"))
                {
                    ResultByteArr = visitBooking.VisitBookingFunc(dataStringArray);

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
                    ResultStr = docSchedule.DocScheduleFunc(dataStringArray);

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
                    boolAnswer = booking.BookingFunc(dataStringArray);

                    if (boolAnswer)
                    {
                        sendData = "BookingTrue";
                    }
                    else
                    {
                        sendData = "BookingFalse";
                    }
                }

                //---------------------Закріплення пацієнта за доктором------------------------
                if (dataStringArray[0].Equals("FixPacient"))
                {
                    boolAnswer = fixPacient.FixPacientFunc(dataStringArray);

                    if (boolAnswer)
                    {
                        sendData = "FixPacientTrue";
                    }
                    else
                    {
                        sendData = "FixPacientFalse";
                    }
                }

                //---------------------Перегляд закріплених пацієнтів------------------------
                if (dataStringArray[0].Equals("MyPacients"))
                {
                    ResultStr = myPacients.MyPacientsFunc(dataStringArray);

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
                    boolAnswer = editLK.EditLKFunc(dataStringArray);

                    if (boolAnswer)
                    {
                        sendData = "EditLKTrue";
                    }
                    else
                    {
                        sendData = "EditLKFalse";
                    }
                }

                //---------------------Додавання рахунку------------------------
                if (dataStringArray[0].Equals("AddBill"))
                {
                    boolAnswer = addBill.AddBillFunc(dataStringArray);

                    if (boolAnswer)
                    {
                        sendData = "AddBillTrue";
                    }
                    else
                    {
                        sendData = "AddBillFalse";
                    }
                }
                if (!dataStringArray[0].Equals("VisitBooking"))
                    listener.Send(Encoding.Unicode.GetBytes(sendData));

                listener.Shutdown(SocketShutdown.Both);
                listener.Close();
            }
        }
    }
}
