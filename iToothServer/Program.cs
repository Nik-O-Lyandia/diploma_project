using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Linq;

namespace iToothServer
{
    class Program
    {
        static void Main(string[] args)
        {
            const string ip = "127.0.0.1";
            const int port = 8080;

            

            string clientType = "";
            string login = "";
            int passwordHesh = 0;
            string pasportSeries = "";
            string pasportNumber = "";
            string name = "";
            string surname = "";
            bool EnterAllowed = false;

            IPEndPoint tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            Socket tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                tcpSocket.Bind(tcpEndPoint);
            }
            catch(Exception ex)
            {
                Console.WriteLine("ERROR: Exception in bind socket: " + ex);
            }
            tcpSocket.Listen(5);

            while(true)
            {
                Socket listener = tcpSocket.Accept();
                byte[] buffer = new byte[256];
                int size = 0;
                StringBuilder data = new StringBuilder();

                do
                {
                    size = listener.Receive(buffer);
                    data.Append(Encoding.Unicode.GetString(buffer, 0 , size));
                }
                while (listener.Available > 0);

                string[] dataStringArray = data.ToString().Split("|");

                if (dataStringArray[0].Equals("LogIn"))
                {
                    clientType = dataStringArray[1];
                    login = dataStringArray[2];
                    passwordHesh = BitConverter.ToInt32(Encoding.Unicode.GetBytes(dataStringArray[3]));
                    EnterAllowed = LogInFunc(dataStringArray);
                }
                if (dataStringArray[0].Equals("SignIn"))
                {
                    clientType = dataStringArray[1];
                    login = dataStringArray[2];
                    passwordHesh = BitConverter.ToInt32(Encoding.Unicode.GetBytes(dataStringArray[3]));
                    pasportSeries = dataStringArray[4];
                    pasportNumber = dataStringArray[5];
                    name = dataStringArray[6];
                    surname = dataStringArray[7];
                }

                //for (int i = 0; i < dataStringArray.Length; i++)
                //{
                //    Console.WriteLine("-- " + dataStringArray[i]);
                //}
                //Console.WriteLine(data);

                if (clientType.Equals("Пацієнт"))
                {
                    PatienClass dataPatient = new PatienClass();
                    Console.WriteLine("----- Patient -----");
                    Console.WriteLine(passwordHesh);
                }
                if (clientType.Equals("Лікар"))
                {
                    DoctorClass dataDoctor = new DoctorClass();
                }


                listener.Send(Encoding.Unicode.GetBytes("Succes"));

                listener.Shutdown(SocketShutdown.Both);
                listener.Close();
            }
        }

        static private bool LogInFunc(string[] dataStringArray)
        {
            using (iToothServerContext iToothServ = new iToothServerContext())
            {
                int EnteredUserId = 0;
                bool EnterAllowed = false;

                var dataPacient = iToothServ.pacient.AsQueryable();
                var dataDoctor = iToothServ.doctor.AsQueryable();

                if (dataStringArray[1].Equals("Пацієнт"))
                {
                    for (int i = 0; i < dataPacient.Count(); i++)
                    {
                        if (dataPacient.Single(a => a.id == (i + 1)).login == dataStringArray[2] && 
                            dataPacient.Single(a => a.id == (i + 1)).passwordHash == BitConverter.ToInt32(Encoding.Unicode.GetBytes(dataStringArray[3])))
                        {
                            EnteredUserId = dataPacient.Single(a => a.id == (i + 1)).id;
                            EnterAllowed = true;
                        }
                    }
                }
                if (dataStringArray[1].Equals("Лікар"))
                {
                    for (int i = 0; i < dataDoctor.Count(); i++)
                    {
                        if (dataDoctor.Single(a => a.id == (i + 1)).login == dataStringArray[2] && 
                            dataDoctor.Single(a => a.id == (i + 1)).passwordHash == BitConverter.ToInt32(Encoding.Unicode.GetBytes(dataStringArray[3])))
                        {
                            EnteredUserId = dataPacient.Single(a => a.id == (i + 1)).id;
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
