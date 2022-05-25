using DiplomClient.SubFuncs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

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

            Col0.Width = GridLength.Auto;
            Col1.Width = new GridLength(0);

            Row0.Height = new GridLength(260);

            UserLogin = login;

            BillCostATextBox.Text = "<100>";
            BillCostBTextBox.Text = "<00>";
        }

        private void LookScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            Col1.Width = new GridLength(ScheduleDataGrid.Width + ScheduleDataGrid.Margin.Right + ScheduleDataGrid.Margin.Left);
            Row0.Height = GridLength.Auto;

            LookScheduleButton.Visibility = Visibility.Hidden;
            MyPacientsButton.Visibility = Visibility.Hidden;
            BillsButton.Visibility = Visibility.Hidden;
            CommentsButton.Visibility = Visibility.Hidden;
            ExitButton.Visibility = Visibility.Hidden;

            FixPacientButton.Visibility = Visibility.Visible;
            PacVisitedButton.Visibility = Visibility.Visible;
            AddVisitButton.Visibility = Visibility.Visible;
            AddVisitConfirmButton.Visibility = Visibility.Visible;
            BackButton.Visibility = Visibility.Visible;

            RefreshSchedule();
        }

        private void RefreshSchedule()
        {
            ScheduleDataGrid.Visibility = Visibility.Visible;

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
                    if (reseption.Date == Convert.ToDateTime(visitData[i][1]).ToShortDateString() && reseption.Time == visitData[i][2] && Convert.ToInt32(visitData[i][4]) == 1)
                    {
                        Reseption visit = new Reseption()
                        {
                            Id = Convert.ToInt32(visitData[i][0]),
                            Date = Convert.ToDateTime(visitData[i][1]).ToShortDateString(),
                            Time = visitData[i][2],
                            TimeId = reseption.TimeId,
                            Login = visitData[i][3],
                            FreeOrNot = false
                        };
                        ScheduleDataGrid.Items[j] = visit;
                    }
                }
            }
        }

        private void PacVisitedButton_Click(object sender, RoutedEventArgs e)
        {
            if (ScheduleDataGrid.SelectedItem != null)
            {
                Reseption reseption = (Reseption)ScheduleDataGrid.SelectedItem;

                byte[] data = Encoding.Unicode.GetBytes("PacVisited");
                data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
                data = data.Concat(Encoding.Unicode.GetBytes(reseption.Id.ToString())).ToArray();

                if (transfer.TransferFunc(data).Equals("True"))
                {
                    MessageBox.Show("Позначка 'Пацієнт з'явився' успішно додана");
                    RefreshSchedule();
                }
                else
                {
                    MessageBox.Show("Редагувати запис не вдалося. Спробуйте будь ласка пізніше");
                }
            }
            else
            {
                MessageBox.Show("Оберіть активний прийом зі списку");
            }

        }

        private void FixPacientButton_Click(object sender, RoutedEventArgs e)
        {
            if (ScheduleDataGrid.SelectedItem != null)
            {
                Reseption reseption = (Reseption)ScheduleDataGrid.SelectedItem;

                byte[] data = Encoding.Unicode.GetBytes("FixPacient");
                data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
                data = data.Concat(Encoding.Unicode.GetBytes(UserLogin)).ToArray();
                data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
                data = data.Concat(Encoding.Unicode.GetBytes(reseption.Login)).ToArray();

                if (transfer.TransferFunc(data).Equals("True"))
                {
                    MessageBox.Show("Запис успішно створено");
                }
                else
                {
                    MessageBox.Show("Закріпити пацієнта не вдалося");
                }
            }
            else
            {
                MessageBox.Show("Оберіть прийом з незакріпленим пацієнтом зі списку");
            }
        }

        private void AddVisitButton_Click(object sender, RoutedEventArgs e)
        {
            if (ScheduleDataGrid.SelectedItem != null)
            {
                ScheduleDataGrid.Visibility = Visibility.Hidden;
                AddVisitButton.Visibility = Visibility.Hidden;
                PacVisitedButton.Visibility = Visibility.Hidden;
                FixPacientButton.Visibility = Visibility.Hidden;

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


                if (transfer.TransferFunc(data).Equals("True"))
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

        private void MyPacientsButton_Click(object sender, RoutedEventArgs e)
        {
            Col1.Width = new GridLength(MyPacDataGrid.Width + MyPacDataGrid.Margin.Right + MyPacDataGrid.Margin.Left);
            Row0.Height = GridLength.Auto;

            LookScheduleButton.Visibility = Visibility.Hidden;
            MyPacientsButton.Visibility = Visibility.Hidden;
            BillsButton.Visibility = Visibility.Hidden;
            CommentsButton.Visibility = Visibility.Hidden;
            ExitButton.Visibility = Visibility.Hidden;

            EditLKButton.Visibility = Visibility.Visible;
            AddBillButton.Visibility = Visibility.Visible;
            BackButton.Visibility = Visibility.Visible;

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

        private void EditLKButton_Click(object sender, RoutedEventArgs e)
        {
            if (MyPacDataGrid.SelectedItem != null)
            {
                EditLKRichTextBox.Visibility = Visibility.Visible;
                AddNoteButton.Visibility = Visibility.Visible;

                EditLKButton.Visibility = Visibility.Hidden;
                AddBillButton.Visibility = Visibility.Hidden;
                MyPacDataGrid.Visibility = Visibility.Hidden;
            }
            else
            {
                MessageBox.Show("Оберіть пацієнта зі списку");
            }
        }

        private void AddNoteButton_Click(object sender, RoutedEventArgs e)
        {
            Pacient pac = (Pacient)MyPacDataGrid.SelectedItem;

            byte[] data = Encoding.Unicode.GetBytes("EditLK");
            data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes(pac.Login)).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes(new TextRange(EditLKRichTextBox.Document.ContentStart, EditLKRichTextBox.Document.ContentEnd).Text)).ToArray();

            if (transfer.TransferFunc(data).Equals("True"))
            {
                MessageBox.Show("Запис успішно додано");
                EditLKRichTextBox.Document.Blocks.Clear();
            }
            else
            {
                MessageBox.Show("Додати запис не вдалося");
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


                if (transfer.TransferFunc(data).Equals("True"))
                {
                    MessageBox.Show("Рахунок успішно додано");
                }
                else
                {
                    MessageBox.Show("Додати рахунок не вдалось. Спробуйте будь ласка пізніше");
                }
            }
            else
            {
                MessageBox.Show("Оберіть пацієнта");
            }
        }

        private void BillsButton_Click(object sender, RoutedEventArgs e)
        {
            Col1.Width = new GridLength(BillsDataGrid.Width + BillsDataGrid.Margin.Right + BillsDataGrid.Margin.Left);
            Row0.Height = GridLength.Auto;

            LookScheduleButton.Visibility = Visibility.Hidden;
            MyPacientsButton.Visibility = Visibility.Hidden;
            BillsButton.Visibility = Visibility.Hidden;
            CommentsButton.Visibility = Visibility.Hidden;
            ExitButton.Visibility = Visibility.Hidden;

            BillsDataGrid.Visibility = Visibility.Visible;
            DeleteBillButton.Visibility = Visibility.Visible;
            BackButton.Visibility = Visibility.Visible;

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
                    Id = Convert.ToInt32(billData[i][0]),
                    Date = billData[i][1].Split(' ')[0],
                    Name = billData[i][2],
                    Cost = Convert.ToDecimal(billData[i][3]),
                    PIB = billData[i][4],
                    Login = billData[i][5]
                };
                BillsDataGrid.Items.Add(bill);
            }
        }

        private void DeleteBillButton_Click(object sender, RoutedEventArgs e)
        {
            if (BillsDataGrid.SelectedItem != null)
            {
                Bill bill = (Bill)BillsDataGrid.SelectedItem;
                string sMessageBoxText = "Ви дійсно хочете видалити рахунок?";
                string sCaption = "Попередження";

                MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
                MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

                MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

                switch (rsltMessageBox)
                {
                    case MessageBoxResult.Yes:
                        byte[] data = Encoding.Unicode.GetBytes("DeleteBill");
                        data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
                        data = data.Concat(Encoding.Unicode.GetBytes(bill.Id.ToString())).ToArray();

                        if (transfer.TransferFunc(data).Equals("True"))
                        {
                            MessageBox.Show("Рахунок успішно видалено");
                        }
                        else
                        {
                            MessageBox.Show("Видалити рахунок не вдалось. Спробуйте будь ласка пізніше");
                        }
                        break;

                    case MessageBoxResult.No:
                        break;
                }
            }
            else
            {
                MessageBox.Show("Оберіть спочатку рахунок");
            }
        }

        private void CommentsButton_Click(object sender, RoutedEventArgs e)
        {
            LookScheduleButton.Visibility = Visibility.Hidden;
            MyPacientsButton.Visibility = Visibility.Hidden;
            BillsButton.Visibility = Visibility.Hidden;
            CommentsButton.Visibility = Visibility.Hidden;
            ExitButton.Visibility = Visibility.Hidden;

            CommentsScrollViewer.Visibility = Visibility.Visible;
            BackButton.Visibility = Visibility.Visible;

            Col1.Width = new GridLength(CommentsScrollViewer.Width + CommentsScrollViewer.Margin.Right + CommentsScrollViewer.Margin.Left);
            Row0.Height = GridLength.Auto;

            byte[] data = Encoding.Unicode.GetBytes("GetComments");
            data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes(UserLogin)).ToArray();

            List<Comment> coms = ObjectReBuilder.ReBuildCommentFromBytes(transfer.TransferFuncByte(data));
            StackPanel commentsStackPanel = new StackPanel();
            commentsStackPanel.HorizontalAlignment = HorizontalAlignment.Center;
            commentsStackPanel.VerticalAlignment = VerticalAlignment.Top;
            commentsStackPanel.Width = CommentsScrollViewer.Width;
            double ratingSum = 0;

            for (int i = 0; i < coms.Count; i++)
            {
                DockPanel commentObjectDockPanel = new DockPanel();
                commentObjectDockPanel.Width = commentsStackPanel.Width;

                TextBlock commetTextTextBlock = new TextBlock();
                TextBlock commetPIBTextBlock = new TextBlock();
                TextBlock commetRatingTextBlock = new TextBlock();

                Thickness myThickness = new Thickness();

                commetTextTextBlock.Text = coms[i].CommentText;
                commetTextTextBlock.Background = new SolidColorBrush(Colors.Aquamarine);
                commetTextTextBlock.TextWrapping = TextWrapping.Wrap;
                myThickness.Bottom = 0;
                myThickness.Left = 0;
                myThickness.Right = 18;
                myThickness.Top = 0;
                commetTextTextBlock.Margin = myThickness;

                commetPIBTextBlock.Text = coms[i].PacientPIB;
                commetPIBTextBlock.Background = new SolidColorBrush(Colors.Aquamarine);
                commetPIBTextBlock.TextWrapping = TextWrapping.Wrap;
                commetPIBTextBlock.HorizontalAlignment = HorizontalAlignment.Left;
                myThickness.Bottom = 5;
                myThickness.Left = 2;
                myThickness.Right = 0;
                myThickness.Top = 0;
                commetPIBTextBlock.Margin = myThickness;

                commetRatingTextBlock.Text = "Оцінка: " + coms[i].Rating.ToString();
                commetRatingTextBlock.Background = new SolidColorBrush(Colors.Aquamarine);
                commetRatingTextBlock.TextWrapping = TextWrapping.Wrap;
                commetRatingTextBlock.HorizontalAlignment = HorizontalAlignment.Right;
                myThickness.Bottom = 5;
                myThickness.Left = 0;
                myThickness.Right = 18;
                myThickness.Top = 0;
                commetRatingTextBlock.Margin = myThickness;

                DockPanel.SetDock(commetTextTextBlock, Dock.Bottom);
                DockPanel.SetDock(commetPIBTextBlock, Dock.Left);
                DockPanel.SetDock(commetRatingTextBlock, Dock.Right);
                commentObjectDockPanel.Children.Add(commetTextTextBlock);
                commentObjectDockPanel.Children.Add(commetPIBTextBlock);
                commentObjectDockPanel.Children.Add(commetRatingTextBlock);

                myThickness.Bottom = 10;
                myThickness.Left = 0;
                myThickness.Right = 0;
                myThickness.Top = 0;
                commentObjectDockPanel.Margin = myThickness;

                commentsStackPanel.Children.Add(commentObjectDockPanel);

                ratingSum += coms[i].Rating;
            }

            CommentsScrollViewer.Content = commentsStackPanel;

            Row0.Height = new GridLength(CommentsScrollViewer.Height + CommentsScrollViewer.Margin.Top + CommentsScrollViewer.Margin.Bottom);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Col0.Width = GridLength.Auto;
            Col1.Width = new GridLength(0);

            Row0.Height = new GridLength(260);

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
            DeleteBillButton.Visibility = Visibility.Hidden;
            BackButton.Visibility = Visibility.Hidden;

            LookScheduleButton.Visibility = Visibility.Visible;
            MyPacientsButton.Visibility = Visibility.Visible;
            BillsButton.Visibility = Visibility.Visible;
            CommentsButton.Visibility = Visibility.Visible;
            ExitButton.Visibility = Visibility.Visible;

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

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
