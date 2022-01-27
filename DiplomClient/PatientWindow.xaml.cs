using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

namespace DiplomClient
{
    /// <summary>
    /// Логика взаимодействия для PatientWindow.xaml
    /// </summary>
    public partial class PatientWindow : Window
    {
        Transfer transfer = new Transfer();
        private string UserLogin;
        DataGridTextColumn[] textColumns;

        public PatientWindow(string login)
        {
            InitializeComponent();

            UserLogin = login;
        }

        private void LKButton_Click(object sender, RoutedEventArgs e)
        {
            BillsButton.Visibility = Visibility.Hidden;
            LKButton.Visibility = Visibility.Hidden;
            MyVisitsButton.Visibility = Visibility.Hidden;
            VisitBookingButton.Visibility = Visibility.Hidden;

            LKRichTextBox.Visibility = Visibility.Visible;
            BackButton.Visibility = Visibility.Visible;

            byte[] data = Encoding.Unicode.GetBytes("LookLK");
            data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes("Пацієнт")).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes(UserLogin)).ToArray();


            LKRichTextBox.Document.Blocks.Clear();
            LKRichTextBox.Document.Blocks.Add(new Paragraph(new Run(transfer.TransferFunc(data))));
        }

        private void BillsButton_Click(object sender, RoutedEventArgs e)
        {
            BillsButton.Visibility = Visibility.Hidden;
            LKButton.Visibility = Visibility.Hidden;
            MyVisitsButton.Visibility = Visibility.Hidden;
            VisitBookingButton.Visibility = Visibility.Hidden;

            InfoTableDataGrid.Visibility = Visibility.Visible;
            BackButton.Visibility = Visibility.Visible;
            PayBillButton.Visibility = Visibility.Visible;

            InfoTableDataGrid.Items.Clear();
            InfoTableDataGrid.Columns.Clear();

            textColumns = new DataGridTextColumn[5];
            textColumns[0] = new DataGridTextColumn();
            textColumns[0].Header = "Дата видачі";
            textColumns[0].Binding = new Binding("Date");
            InfoTableDataGrid.Columns.Add(textColumns[0]);

            textColumns[1] = new DataGridTextColumn();
            textColumns[1].Header = "Назва";
            textColumns[1].Binding = new Binding("Name");
            InfoTableDataGrid.Columns.Add(textColumns[1]);

            textColumns[2] = new DataGridTextColumn();
            textColumns[2].Header = "Ціна";
            textColumns[2].Binding = new Binding("Cost");
            InfoTableDataGrid.Columns.Add(textColumns[2]);

            textColumns[3] = new DataGridTextColumn();
            textColumns[3].Header = "ПІБ";
            textColumns[3].Binding = new Binding("PIB");
            InfoTableDataGrid.Columns.Add(textColumns[3]);

            textColumns[4] = new DataGridTextColumn();      // TODO: Замінити логін на щось іще
            textColumns[4].Header = "Логін";
            textColumns[4].Binding = new Binding("Login");
            InfoTableDataGrid.Columns.Add(textColumns[4]);

            byte[] data = Encoding.Unicode.GetBytes("MyBills");
            data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes("Пацієнт")).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes(UserLogin)).ToArray();


            string[] answerData = transfer.TransferFunc(data).Split('$');
            string[][] billData = new string[answerData.Length - 1][];
            for (int i = 0; i < answerData.Length - 1; i++)
            {
                billData[i] = answerData[i].Split('|');
                Bill bill = new Bill()
                {
                    Date = billData[i][0].Split(' ')[0],
                    Name = billData[i][1],
                    Cost = Convert.ToDecimal(billData[i][2]),
                    PIB = billData[i][3],
                    Login = billData[i][4]
                };
                InfoTableDataGrid.Items.Add(bill);
            }

        }

        private void VisitBookingButton_Click(object sender, RoutedEventArgs e)
        {
            BillsButton.Visibility = Visibility.Hidden;
            LKButton.Visibility = Visibility.Hidden;
            MyVisitsButton.Visibility = Visibility.Hidden;
            VisitBookingButton.Visibility = Visibility.Hidden;

            MainLable.Visibility = Visibility.Visible;
            DocImage.Visibility = Visibility.Visible;
            PIBLable.Visibility = Visibility.Visible;
            ExperienceLable.Visibility = Visibility.Visible;
            DegreeLable.Visibility = Visibility.Visible;
            CommentsLable.Visibility = Visibility.Visible;
            ScheduleDataGrid.Visibility = Visibility.Visible;
            CommentsDataGrid.Visibility = Visibility.Visible;
            DoctorsListDataGrid.Visibility = Visibility.Visible;
            ScheduleLable.Visibility = Visibility.Visible;
            BookingButton.Visibility = Visibility.Visible;
            BackButton.Visibility = Visibility.Visible;

            byte[] data = Encoding.Unicode.GetBytes("VisitBooking");
            data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes("Пацієнт")).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes(UserLogin)).ToArray();

            string[] answerData = transfer.TransferFunc(data).Split('$');
            string[][] docsData = new string[answerData.Length - 1][];
            for (int i = 0; i < answerData.Length - 1; i++)
            {
                docsData[i] = answerData[i].Split('|');
                Doctor doc = new Doctor()
                {
                    Id = Convert.ToInt32(docsData[i][0]),
                    Name = docsData[i][1],
                    Surname = docsData[i][2],
                    Login = docsData[i][2]
                };
                DoctorsListDataGrid.Items.Add(doc);
            }
        }

        private void PayBillButton_Click(object sender, RoutedEventArgs e)
        {
            byte[] data = Encoding.Unicode.GetBytes("PayBill");
            data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes("Пацієнт")).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes(UserLogin)).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();

            Bill bill = (Bill)InfoTableDataGrid.SelectedItem;
            data = data.Concat(Encoding.Unicode.GetBytes(bill.Name)).ToArray();

            if(transfer.TransferFunc(data).Equals("PayBillTrue"))
            {
                MessageBox.Show("Сплата пройшла успішно");
            }
            else
            {
                MessageBox.Show("Сплата не виконана");
            }
        }

        private void MyVisitsButton_Click(object sender, RoutedEventArgs e)
        {
            BillsButton.Visibility = Visibility.Hidden;
            LKButton.Visibility = Visibility.Hidden;
            MyVisitsButton.Visibility = Visibility.Hidden;
            VisitBookingButton.Visibility = Visibility.Hidden;

            InfoTableDataGrid.Visibility = Visibility.Visible;
            BackButton.Visibility = Visibility.Visible;

            InfoTableDataGrid.Items.Clear();
            InfoTableDataGrid.Columns.Clear();

            textColumns = new DataGridTextColumn[5];
            textColumns[0] = new DataGridTextColumn();
            textColumns[0].Header = "Дата прийому";
            textColumns[0].Binding = new Binding("Date");
            InfoTableDataGrid.Columns.Add(textColumns[0]);

            textColumns[1] = new DataGridTextColumn();
            textColumns[1].Header = "Час прийому";
            textColumns[1].Binding = new Binding("Time");
            InfoTableDataGrid.Columns.Add(textColumns[1]);

            textColumns[3] = new DataGridTextColumn();
            textColumns[3].Header = "ПІБ Лікаря";
            textColumns[3].Binding = new Binding("PIB");
            InfoTableDataGrid.Columns.Add(textColumns[3]);

            textColumns[4] = new DataGridTextColumn();      // TODO: Замінити логін на КАБІНЕТ лікаря
            textColumns[4].Header = "Логін Лікаря";
            textColumns[4].Binding = new Binding("Login");
            InfoTableDataGrid.Columns.Add(textColumns[4]);

            byte[] data = Encoding.Unicode.GetBytes("MyVisits");
            data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes("Пацієнт")).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes(UserLogin)).ToArray();

            string[] answerData = transfer.TransferFunc(data).Split('$');
            string[][] visitData = new string[answerData.Length - 1][];
            for (int i = 0; i < answerData.Length - 1; i++)
            {
                visitData[i] = answerData[i].Split('|');
                Reseption visit = new Reseption()
                {
                    Date = Convert.ToDateTime(visitData[i][0]).ToShortDateString(),
                    Time = visitData[i][1],
                    PIB = visitData[i][2],
                    Login = visitData[i][3]
                };
                InfoTableDataGrid.Items.Add(visit);
            }

        }

        private void DoctorsListDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            RefreshSchedule();
        }

        private void RefreshSchedule()
        {
            ScheduleDataGrid.Items.Clear();

            Doctor doc = (Doctor)DoctorsListDataGrid.SelectedItem;
            DateTime dateTimeNow = DateTime.Now;
            string time = "";

            for (int i = 0; i < 55; i++)
            {
                if (i % 8 == 0 && i != 0)
                {
                    dateTimeNow = dateTimeNow.AddDays(1);
                }
                time = (9 + i % 8) + ":00-" + (10 + i % 8) + ":00";
                Reseption visit = new Reseption()
                {
                    Date = dateTimeNow.ToShortDateString(),
                    Time = time,
                    TimeId = i % 8,
                    Login = doc.Login,
                    FreeOrNot = true
                };
                ScheduleDataGrid.Items.Add(visit);
            }

            byte[] data = Encoding.Unicode.GetBytes("DocSchedule");
            data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes("Пацієнт")).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes(doc.Login)).ToArray();

            string[] answerData = transfer.TransferFunc(data).Split('$');

            string[][] visitData = new string[answerData.Length - 1][];
            for (int i = 0; i < answerData.Length - 1; i++)
            {
                visitData[i] = answerData[i].Split('|');
                for (int j = 0; j < ScheduleDataGrid.Items.Count; j++)
                {
                    Reseption reseption = (Reseption)ScheduleDataGrid.Items[j];
                    if (reseption.Date == Convert.ToDateTime(visitData[i][0]).ToShortDateString() && reseption.Time == visitData[i][1])
                    {
                        Reseption visit = new Reseption()
                        {
                            Date = Convert.ToDateTime(visitData[i][0]).ToShortDateString(),
                            Time = visitData[i][1],
                            TimeId = reseption.TimeId,
                            PIB = visitData[i][2],
                            Login = visitData[i][3],
                            FreeOrNot = false
                        };
                        ScheduleDataGrid.Items[j] = visit;
                    }
                }
            }
        }

        private void BookingButton_Click(object sender, RoutedEventArgs e)
        {
            Reseption reseption = (Reseption)ScheduleDataGrid.SelectedItem;
            
            byte[] data = Encoding.Unicode.GetBytes("Booking");
            data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes(UserLogin)).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes(reseption.Login)).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes(reseption.Date)).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes(reseption.TimeId.ToString())).ToArray();


            if (transfer.TransferFunc(data).Equals("BookingTrue"))
            {
                MessageBox.Show("Запис успішно створено");
                RefreshSchedule();
            }
            else
            {
                MessageBox.Show("Записатись не вдалось");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            BillsButton.Visibility = Visibility.Visible;
            BookingButton.Visibility = Visibility.Visible;
            LKButton.Visibility = Visibility.Visible;
            MyVisitsButton.Visibility = Visibility.Visible;
            VisitBookingButton.Visibility = Visibility.Visible;

            DocImage.Visibility = Visibility.Hidden;
            PIBLable.Visibility = Visibility.Hidden;
            ExperienceLable.Visibility = Visibility.Hidden;
            DegreeLable.Visibility = Visibility.Hidden;
            CommentsLable.Visibility = Visibility.Hidden;
            ScheduleDataGrid.Visibility = Visibility.Hidden;
            CommentsDataGrid.Visibility = Visibility.Hidden;
            DoctorsListDataGrid.Visibility = Visibility.Hidden;
            ScheduleLable.Visibility = Visibility.Hidden;
            BookingButton.Visibility = Visibility.Hidden;
            MainLable.Visibility = Visibility.Hidden;
            InfoTableDataGrid.Visibility = Visibility.Hidden;
            PayBillButton.Visibility = Visibility.Hidden;
            LKRichTextBox.Visibility = Visibility.Hidden;

            BackButton.Visibility = Visibility.Hidden;
        }

        // TODO: добавить обработку КОМЕНТАРИЕВ, ОЦЕНКИ и ОПЫТА РОБОТЫ
    }

}
