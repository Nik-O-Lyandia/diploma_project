using System;
using System.Linq;
using System.Text;
using System.Windows;

namespace DiplomClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Transfer transfer = new Transfer();
        public MainWindow()
        {
            InitializeComponent();

            WhoAreYouComboBox.Items.Add("Пацієнт");
            WhoAreYouComboBox.Items.Add("Лікар");

            PasswordCheckLable.Visibility = Visibility.Hidden;
            PasswordCheckTextBox.Visibility = Visibility.Hidden;
            PassportLable.Visibility = Visibility.Hidden;
            PassportSeriesTextBox.Visibility = Visibility.Hidden;
            PasportNumberTextBox.Visibility = Visibility.Hidden;
            SurnameLable.Visibility = Visibility.Hidden;
            SurnameTextBox.Visibility = Visibility.Hidden;
            NameLable.Visibility = Visibility.Hidden;
            NameTexBox.Visibility = Visibility.Hidden;
            ConfirmButton.Visibility = Visibility.Hidden;
            BackButton.Visibility = Visibility.Hidden;

            AuthorizationWindow.Height = 420;

            Row1.Height = new GridLength(0);
        }

        private void RegisterationButton_Click(object sender, RoutedEventArgs e)
        {
            PasswordCheckLable.Visibility = Visibility.Visible;
            PasswordCheckTextBox.Visibility = Visibility.Visible;
            PassportLable.Visibility = Visibility.Visible;
            PassportSeriesTextBox.Visibility = Visibility.Visible;
            PasportNumberTextBox.Visibility = Visibility.Visible;
            SurnameLable.Visibility = Visibility.Visible;
            SurnameTextBox.Visibility = Visibility.Visible;
            NameLable.Visibility = Visibility.Visible;
            NameTexBox.Visibility = Visibility.Visible;
            ConfirmButton.Visibility = Visibility.Visible;
            BackButton.Visibility = Visibility.Visible;

            EnterButton.Visibility = Visibility.Hidden;
            RegisterationButton.Visibility = Visibility.Hidden;
            RegistrationLable.Visibility = Visibility.Hidden;

            AuthorizationWindow.Height = 630;

            Row1.Height = GridLength.Auto;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            PasswordCheckLable.Visibility = Visibility.Hidden;
            PasswordCheckTextBox.Visibility = Visibility.Hidden;
            PassportLable.Visibility = Visibility.Hidden;
            PassportSeriesTextBox.Visibility = Visibility.Hidden;
            PasportNumberTextBox.Visibility = Visibility.Hidden;
            SurnameLable.Visibility = Visibility.Hidden;
            SurnameTextBox.Visibility = Visibility.Hidden;
            NameLable.Visibility = Visibility.Hidden;
            NameTexBox.Visibility = Visibility.Hidden;
            ConfirmButton.Visibility = Visibility.Hidden;
            BackButton.Visibility = Visibility.Hidden;

            EnterButton.Visibility = Visibility.Visible;
            RegisterationButton.Visibility = Visibility.Visible;
            RegistrationLable.Visibility = Visibility.Visible;

            AuthorizationWindow.Height = 420;

            Row1.Height = new GridLength(0);
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            bool RegistrAllowed = false;

            //------------------------------------ Запись данных в базу------------------------------------
            if (LoginTextBox.Text != "" && PasswordCheckTextBox.Text != "" &&
                PasswordTextBox.Text != "" && PassportSeriesTextBox.Text != "" && 
                PasportNumberTextBox.Text != "" && SurnameTextBox.Text != "" && NameTexBox.Text != "")
            {
                if (PasswordTextBox.Text == PasswordCheckTextBox.Text)
                {
                    RegistrAllowed = true;
                } 
                else
                    MessageBox.Show("Пароль не співпадає");
            }
            else
                MessageBox.Show("Усі поля мають бути заповнені");

            if (RegistrAllowed == true)
            {
                byte[] data = Encoding.Unicode.GetBytes("SignIn");
                data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
                data = data.Concat(Encoding.Unicode.GetBytes(WhoAreYouComboBox.Text)).ToArray();
                data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
                data = data.Concat(Encoding.Unicode.GetBytes(LoginTextBox.Text)).ToArray();
                data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();

                int hesh = 0;
                for (int j = 0; j < PasswordTextBox.Text.Length; j++)
                {
                    hesh += Convert.ToInt32(PasswordTextBox.Text[j]) % 1234 * ((j + 1) % 5) + j * j;
                }
                data = data.Concat(BitConverter.GetBytes(hesh)).ToArray();

                data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
                data = data.Concat(Encoding.Unicode.GetBytes(PassportSeriesTextBox.Text)).ToArray();
                data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
                data = data.Concat(Encoding.Unicode.GetBytes(PasportNumberTextBox.Text)).ToArray();
                data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
                data = data.Concat(Encoding.Unicode.GetBytes(NameTexBox.Text)).ToArray();
                data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
                data = data.Concat(Encoding.Unicode.GetBytes(SurnameTextBox.Text)).ToArray();

                MessageBox.Show( transfer.TransferFunc(data) );
            }
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            bool LogInAllowed = false;

            if (LoginTextBox.Text != "" && PasswordTextBox.Text != "")
            {
                LogInAllowed = true;
            }
            else
                MessageBox.Show("Усі поля мають бути заповнені");

            if (LogInAllowed)           // TODO: Добавить в БД в таблицю Пациент свойство Online<bool>
            {
                byte[] data = Encoding.Unicode.GetBytes("LogIn");
                data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
                data = data.Concat(Encoding.Unicode.GetBytes(WhoAreYouComboBox.Text)).ToArray();
                data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();
                data = data.Concat(Encoding.Unicode.GetBytes(LoginTextBox.Text)).ToArray();
                data = data.Concat(Encoding.Unicode.GetBytes("|")).ToArray();

                int hesh = 0;
                for (int j = 0; j < PasswordTextBox.Text.Length; j++)
                {
                    hesh += Convert.ToInt32(PasswordTextBox.Text[j]) % 1234 * ((j + 1) % 5) + j * j;
                }

                data = data.Concat(BitConverter.GetBytes(hesh)).ToArray();

                //Console.WriteLine(hesh);

                if( transfer.TransferFunc(data).Equals("LogInTrue") )
                {
                    if (WhoAreYouComboBox.Text.Equals("Пацієнт"))
                    {
                        PatientWindow patientWindow = new PatientWindow(LoginTextBox.Text);
                        this.Hide();
                        patientWindow.ShowDialog();
                        this.Show();
                    }
                    if (WhoAreYouComboBox.Text.Equals("Лікар"))
                    {
                        DoctorWindow doctorWindow = new DoctorWindow(LoginTextBox.Text);
                        this.Hide();
                        doctorWindow.ShowDialog();
                        this.Show();
                    }
                }
                else
                    MessageBox.Show("Невірний логін або пароль");
            }
        }
    }
}
