using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DiplomClient
{
    /// <summary>
    /// Логика взаимодействия для DoctorWindow.xaml
    /// </summary>
    public partial class DoctorWindow : Window
    {
        Transfer transfer = new Transfer();
        private string UserLogin;
        public DoctorWindow(string login)
        {
            InitializeComponent();

            UserLogin = login;

            BillCostATextBox.Text = "<100>";
            BillCostBTextBox.Text = "<00>";
        }

        private void LookScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            MainMenuStackPanel.Visibility = Visibility.Hidden;
            FixPacientButton.Visibility = Visibility.Visible;

            RefreshSchedule();
        }

        private void RefreshSchedule()
        {
            ScheduleDataGrid.Visibility = Visibility.Visible;
            AddVisitButton.Visibility = Visibility.Visible;

            ScheduleDataGrid.Items.Clear();

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
                    FreeOrNot = true
                };
                ScheduleDataGrid.Items.Add(visit);
            }

            byte[] data = Encoding.Unicode.GetBytes("DocSchedule");
            data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes("Лікар")).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes(UserLogin)).ToArray();

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
                            Login = visitData[i][2],
                            FreeOrNot = false
                        };
                        ScheduleDataGrid.Items[j] = visit;
                    }
                }
            }
        }

        private void PacVisitedButton_Click(object sender, RoutedEventArgs e)
        {
            Reseption reseption = (Reseption)ScheduleDataGrid.SelectedItem;

            MessageBox.Show(reseption.Login);

            // TODO: Пацієнт з'явився на прийом

        }

        private void FixPacientButton_Click(object sender, RoutedEventArgs e)
        {
            Reseption reseption = (Reseption)ScheduleDataGrid.SelectedItem;

            byte[] data = Encoding.Unicode.GetBytes("FixPacient");
            data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes(UserLogin)).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes(reseption.Login)).ToArray();

            if (transfer.TransferFunc(data).Equals("FixPacientTrue"))
            {
                MessageBox.Show("Запис успішно створено");
            }
            else
            {
                MessageBox.Show("Закріпити пацієнта не вдалося");
            }
        }

        private void MyPacientsButton_Click(object sender, RoutedEventArgs e)
        {
            MainMenuStackPanel.Visibility = Visibility.Hidden;

            EditLKButton.Visibility = Visibility.Visible;
            AddBillButton.Visibility = Visibility.Visible;

            ShowPacients();
        }

        private void ShowPacients()
        {
            MyPacDataGrid.Visibility = Visibility.Visible;

            MyPacDataGrid.Items.Clear();

            byte[] data = Encoding.Unicode.GetBytes("MyPacients");
            data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes(UserLogin)).ToArray();

            string[] answerData = transfer.TransferFunc(data).Split('$');

            string[][] myPacsData = new string[answerData.Length - 1][];
            for (int i = 0; i < answerData.Length - 1; i++)
            {
                myPacsData[i] = answerData[i].Split('|');
                Pacient pacient = new Pacient()
                {
                    Name = myPacsData[i][0],
                    Surname = myPacsData[i][1],
                    Login = myPacsData[i][2]
                };
                MyPacDataGrid.Items.Add(pacient);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MyPacDataGrid.Visibility = Visibility.Hidden;
            ScheduleDataGrid.Visibility = Visibility.Hidden;
            BillsDataGrid.Visibility = Visibility.Hidden;
            EditLKRichTextBox.Visibility = Visibility.Hidden;

            AddVisitButton.Visibility = Visibility.Hidden;
            AddNoteButton.Visibility = Visibility.Hidden;
            PacVisitedButton.Visibility = Visibility.Hidden;
            AddVisitConfirmButton.Visibility = Visibility.Hidden;
            AddBillConfirmButton.Visibility = Visibility.Hidden;
            FixPacientButton.Visibility = Visibility.Hidden;
            AddBillButton.Visibility = Visibility.Hidden;
            EditLKButton.Visibility = Visibility.Hidden;
            BiilCanvas.Visibility = Visibility.Hidden;

            MainMenuStackPanel.Visibility = Visibility.Visible;

        }

        private void BillsButton_Click(object sender, RoutedEventArgs e)    // TODO: Deleting bill
        {
            MainMenuStackPanel.Visibility = Visibility.Hidden;
            BillsDataGrid.Visibility = Visibility.Visible;

            BillsDataGrid.Items.Clear();

            byte[] data = Encoding.Unicode.GetBytes("MyBills");
            data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes("Лікар")).ToArray();
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
                BillsDataGrid.Items.Add(bill);
            }
        }

        private void EditLKButton_Click(object sender, RoutedEventArgs e)
        {
            EditLKRichTextBox.Visibility = Visibility.Visible;
            AddNoteButton.Visibility = Visibility.Visible;
            EditLKButton.Visibility = Visibility.Hidden;
            MyPacDataGrid.Visibility = Visibility.Hidden;
        }

        private void AddNoteButton_Click(object sender, RoutedEventArgs e)
        {
            Pacient pac = (Pacient)MyPacDataGrid.SelectedItem;

            byte[] data = Encoding.Unicode.GetBytes("EditLK");
            data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes(pac.Login)).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes(new TextRange(EditLKRichTextBox.Document.ContentStart, EditLKRichTextBox.Document.ContentEnd).Text)).ToArray();

            if (transfer.TransferFunc(data).Equals("EditLKTrue"))
            {
                MessageBox.Show("Запис успішно додано");
                EditLKRichTextBox.Document.Blocks.Clear();
            }
            else
            {
                MessageBox.Show("Додати запис не вдалося");
            }

        }

        private void AddVisitButton_Click(object sender, RoutedEventArgs e)
        {
            if (ScheduleDataGrid.SelectedItem != null)
            {
                ScheduleDataGrid.Visibility = Visibility.Hidden;
                AddVisitButton.Visibility = Visibility.Hidden;
                PacVisitedButton.Visibility = Visibility.Visible;
                AddVisitConfirmButton.Visibility = Visibility.Visible;

                ShowPacients();
            }
            else
            {
                MessageBox.Show("Оберіть дату/час спочатку");
            }
        }

        private void AddVisitConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            Reseption reseption = (Reseption)ScheduleDataGrid.SelectedItem;
            Pacient pac = (Pacient)MyPacDataGrid.SelectedItem;

            if (pac != null)
            {
                byte[] data = Encoding.Unicode.GetBytes("Booking");
                data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
                data = data.Concat(Encoding.Unicode.GetBytes(pac.Login)).ToArray();
                data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
                data = data.Concat(Encoding.Unicode.GetBytes(UserLogin)).ToArray();
                data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
                data = data.Concat(Encoding.Unicode.GetBytes(reseption.Date)).ToArray();
                data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
                data = data.Concat(Encoding.Unicode.GetBytes(reseption.TimeId.ToString())).ToArray();


                if (transfer.TransferFunc(data).Equals("BookingTrue"))
                {
                    MessageBox.Show("Запис успішно створено");
                    MyPacDataGrid.Visibility = Visibility.Hidden;
                    RefreshSchedule();
                }
                else
                {
                    MessageBox.Show("Записати пацієнта не вдалось");
                }
            }
            else
            {
                MessageBox.Show("Оберіть пацієнта");
            }
        }

        private void AddBillButton_Click(object sender, RoutedEventArgs e)
        {
            if (MyPacDataGrid.SelectedItem != null)
            {
                MyPacDataGrid.Visibility = Visibility.Hidden;
                AddBillButton.Visibility = Visibility.Hidden;
                EditLKButton.Visibility = Visibility.Hidden;

                BiilCanvas.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Оберіть спочатку пацієнта");
            }
        }

        private void AddBillConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            Pacient pac = (Pacient)MyPacDataGrid.SelectedItem;

            if (pac != null)
            {
                byte[] data = Encoding.Unicode.GetBytes("AddBill");
                data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
                data = data.Concat(Encoding.Unicode.GetBytes(pac.Login)).ToArray();
                data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
                data = data.Concat(Encoding.Unicode.GetBytes(UserLogin)).ToArray();
                data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
                data = data.Concat(Encoding.Unicode.GetBytes(BillNameTextBox.Text)).ToArray();
                data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
                data = data.Concat(Encoding.Unicode.GetBytes(BillCostATextBox.Text + "," + BillCostBTextBox.Text)).ToArray();


                if (transfer.TransferFunc(data).Equals("AddBillTrue"))
                {
                    MessageBox.Show("Запис успішно створено");
                    MyPacDataGrid.Visibility = Visibility.Hidden;
                    RefreshSchedule();
                }
                else
                {
                    MessageBox.Show("Записати пацієнта не вдалось");
                }
            }
            else
            {
                MessageBox.Show("Оберіть пацієнта");
            }
        }

        private void BillCostATextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (BillCostATextBox.Text == "<100>")
                BillCostATextBox.Text = "";
        }

        private void BillCostATextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if(BillCostATextBox.Text == "") 
                BillCostATextBox.Text = "<100>";
        }

        private void BillCostBTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (BillCostBTextBox.Text == "<00>")
                BillCostBTextBox.Text = "";
        }

        private void BillCostBTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (BillCostBTextBox.Text == "")
                BillCostBTextBox.Text = "<00>";
        }
    }
}
