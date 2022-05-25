using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using DiplomClient.SubFuncs;

namespace DiplomClient
{
    /// <summary>
    /// Логика взаимодействия для PatientWindow.xaml
    /// </summary>
    public partial class PatientWindow : Window
    {
        Transfer transfer = new Transfer();
        private string UserLogin;

        public PatientWindow(string login)
        {
            InitializeComponent();

            Col0.Width = GridLength.Auto;
            Col1.Width = new GridLength(0);
            Col2.Width = new GridLength(0);

            Row0.Height = new GridLength(260);
            Row1.Height = new GridLength(0);
            Row2.Height = new GridLength(0);

            UserLogin = login;
        }

        private void VisitBookingButton_Click(object sender, RoutedEventArgs e)
        {
            Col0.Width = GridLength.Auto;
            Col1.Width = GridLength.Auto;
            Col2.Width = new GridLength(DoctorsListDataGrid.Width + DoctorsListDataGrid.Margin.Right + DoctorsListDataGrid.Margin.Left);

            Row0.Height = new GridLength(DoctorsListDataGrid.Height + DoctorsListDataGrid.Margin.Top + DoctorsListDataGrid.Margin.Bottom);
            Row1.Height = new GridLength(0);
            Row2.Height = new GridLength(0);

            BillsButton.Visibility = Visibility.Hidden;
            LKButton.Visibility = Visibility.Hidden;
            MyVisitsButton.Visibility = Visibility.Hidden;
            VisitBookingButton.Visibility = Visibility.Hidden;
            ExitButton.Visibility = Visibility.Hidden;

            MainLable.Visibility = Visibility.Visible;
            DocImage.Visibility = Visibility.Visible;
            DoctorsListDataGrid.Visibility = Visibility.Visible;
            BackButton.Visibility = Visibility.Visible;


            DoctorsListDataGrid.Items.Clear();

            byte[] data = Encoding.Unicode.GetBytes("VisitBooking");
            data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes("Пацієнт")).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes(UserLogin)).ToArray();

            List<Doctor> docs = ObjectReBuilder.ReBuildDoctorFromBytes(transfer.TransferFuncByte(data));

            for (int i = 0; i < docs.Count; i++)
            {
                DoctorsListDataGrid.Items.Add(docs[i]);
            }
        }

        private void BillsButton_Click(object sender, RoutedEventArgs e)
        {
            Col0.Width = GridLength.Auto;
            Col1.Width = new GridLength(0);
            Col2.Width = GridLength.Auto;

            Row0.Height = GridLength.Auto;
            Row1.Height = new GridLength(0);
            Row2.Height = new GridLength(0);

            BillsButton.Visibility = Visibility.Hidden;
            LKButton.Visibility = Visibility.Hidden;
            MyVisitsButton.Visibility = Visibility.Hidden;
            VisitBookingButton.Visibility = Visibility.Hidden;
            ExitButton.Visibility = Visibility.Hidden;

            MyBillsDataGrid.Visibility = Visibility.Visible;
            BackButton.Visibility = Visibility.Visible;
            PayBillButton.Visibility = Visibility.Visible;

            MyBillsDataGrid.Items.Clear();

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
                MyBillsDataGrid.Items.Add(bill);
            }

        }

        private void MyVisitsButton_Click(object sender, RoutedEventArgs e)
        {
            Col0.Width = GridLength.Auto;
            Col1.Width = new GridLength(0);
            Col2.Width = GridLength.Auto;

            Row0.Height = GridLength.Auto;
            Row1.Height = new GridLength(0);
            Row2.Height = new GridLength(0);

            BillsButton.Visibility = Visibility.Hidden;
            LKButton.Visibility = Visibility.Hidden;
            MyVisitsButton.Visibility = Visibility.Hidden;
            VisitBookingButton.Visibility = Visibility.Hidden;
            ExitButton.Visibility = Visibility.Hidden;

            MyVisitsDataGrid.Visibility = Visibility.Visible;
            BackButton.Visibility = Visibility.Visible;

            MyVisitsDataGrid.Items.Clear();

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
                    Id = Convert.ToInt32(visitData[i][0]),
                    Date = Convert.ToDateTime(visitData[i][1]).ToShortDateString(),
                    Time = visitData[i][2],
                    PIB = visitData[i][3],
                    Status = visitData[i][4]
                };
                MyVisitsDataGrid.Items.Add(visit);
            }

        }

        private void LKButton_Click(object sender, RoutedEventArgs e)
        {
            Col0.Width = GridLength.Auto;
            Col1.Width = new GridLength(0);
            Col2.Width = GridLength.Auto;

            Row0.Height = new GridLength(0);
            Row1.Height = GridLength.Auto;
            Row2.Height = new GridLength(0);

            BillsButton.Visibility = Visibility.Hidden;
            LKButton.Visibility = Visibility.Hidden;
            ExitButton.Visibility = Visibility.Hidden;
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

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PayBillButton_Click(object sender, RoutedEventArgs e)
        {
            byte[] data = Encoding.Unicode.GetBytes("PayBill");
            data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes("Пацієнт")).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
            data = data.Concat(Encoding.Unicode.GetBytes(UserLogin)).ToArray();

            Bill bill = (Bill)MyBillsDataGrid.SelectedItem;
            data = data.Concat(Encoding.Unicode.GetBytes(bill.Name)).ToArray();

            if(transfer.TransferFunc(data).Equals("True"))
            {
                MessageBox.Show("Сплата пройшла успішно");
            }
            else
            {
                MessageBox.Show("Сплата не виконана");
            }
        }

        private void DoctorsListDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (DoctorsListDataGrid.Items.Count != 0)
            {
                RefreshSchedule();
                Doctor doc = (Doctor)DoctorsListDataGrid.SelectedItem;

                byte[] data = Encoding.Unicode.GetBytes("GetComments");
                data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
                data = data.Concat(Encoding.Unicode.GetBytes(doc.Login)).ToArray();

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

                PIBLable.Content = doc.Name + " " + doc.Surname;
                ExperienceLable.Content = "Досвід: " + doc.Experience.ToString() + " р.";
                DegreeLable.Content = "Рейтинг: " + (ratingSum/ coms.Count).ToString();

                Row1.Height = new GridLength(ScheduleDataGrid.Height + ScheduleDataGrid.Margin.Top + ScheduleDataGrid.Margin.Bottom);
                Row2.Height = GridLength.Auto;

                PIBLable.Visibility = Visibility.Visible;
                ExperienceLable.Visibility = Visibility.Visible;
                DegreeLable.Visibility = Visibility.Visible;
                CommentsLable.Visibility = Visibility.Visible;
                ScheduleDataGrid.Visibility = Visibility.Visible;
                ScheduleLable.Visibility = Visibility.Visible;

                BookingButton.Visibility = Visibility.Visible;
            }
        }

        private void RefreshSchedule()
        {
            ScheduleDataGrid.Items.Clear();

            Doctor doc = (Doctor)DoctorsListDataGrid.SelectedItem;
            DateTime dateTimeNow = DateTime.Now;
            string time = "";
            DocImage.Source = LoadImage.LoadImageFunc(doc.DocFoto);

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


            if (transfer.TransferFunc(data).Equals("True"))
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
            Col0.Width = GridLength.Auto;
            Col1.Width = new GridLength(0);
            Col2.Width = new GridLength(0);

            Row0.Height = new GridLength(260);
            Row1.Height = new GridLength(0);
            Row2.Height = new GridLength(0);

            BillsButton.Visibility = Visibility.Visible;
            BookingButton.Visibility = Visibility.Visible;
            LKButton.Visibility = Visibility.Visible;
            MyVisitsButton.Visibility = Visibility.Visible;
            VisitBookingButton.Visibility = Visibility.Visible;
            ExitButton.Visibility = Visibility.Visible;

            DocImage.Source = null;
            DocImage.Visibility = Visibility.Hidden;
            PIBLable.Visibility = Visibility.Hidden;
            ExperienceLable.Visibility = Visibility.Hidden;
            DegreeLable.Visibility = Visibility.Hidden;
            CommentsLable.Visibility = Visibility.Hidden;
            ScheduleDataGrid.Visibility = Visibility.Hidden;
            DoctorsListDataGrid.Visibility = Visibility.Hidden;
            ScheduleLable.Visibility = Visibility.Hidden;
            BookingButton.Visibility = Visibility.Hidden;
            MainLable.Visibility = Visibility.Hidden;
            MyBillsDataGrid.Visibility = Visibility.Hidden;
            MyVisitsDataGrid.Visibility = Visibility.Hidden;
            PayBillButton.Visibility = Visibility.Hidden;
            LKRichTextBox.Visibility = Visibility.Hidden;
            DeleteVisitButton.Visibility = Visibility.Hidden;

            BackButton.Visibility = Visibility.Hidden;
        }

        private void DeleteVisitButton_Click(object sender, RoutedEventArgs e)
        {
            Reseption res = (Reseption)MyVisitsDataGrid.SelectedItem;
            if (res.Status == "Активний")
            {
                string sMessageBoxText = "Ви дійсно хочете видалити запис?";
                string sCaption = "Попередження";

                MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
                MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

                MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

                switch (rsltMessageBox)
                {
                    case MessageBoxResult.Yes:
                        byte[] data = Encoding.Unicode.GetBytes("DeleteReseption");
                        data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
                        data = data.Concat(Encoding.Unicode.GetBytes(res.Id.ToString())).ToArray();

                        if (transfer.TransferFunc(data).Equals("True"))
                        {
                            MessageBox.Show("Візит успішно відмінено");
                        }
                        else
                        {
                            MessageBox.Show("Відмінити візит не вдалось. Спробуйте будь ласка пізніше");
                        }
                        break;

                    case MessageBoxResult.No:
                        break;
                }
            }
            else
            {
                MessageBox.Show("Обраний запис не є активним");
            }
        }

        private void MyVisitsDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DeleteVisitButton.Visibility = Visibility.Visible;
        }
    }

}
