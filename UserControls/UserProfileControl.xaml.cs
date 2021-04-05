using Client_MilkAndMeat.DB;
using Client_MilkAndMeat.Users;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client_MilkAndMeat.UserControls
{
    /// <summary>
    /// Логика взаимодействия для UserProfileControl.xaml
    /// </summary>
    public partial class UserProfileControl : UserControl
    {
        Manufacturer mainManufacturer;
        public UserProfileControl(ref Manufacturer manufacturer)
        {
            this.mainManufacturer = manufacturer;
            InitializeComponent();
            InitUserData();
        }
        public void InitUserData()
        {
            NameText.Text = (mainManufacturer.PIB).Split(' ')[1];
            SurnameText.Text = (mainManufacturer.PIB).Split(' ')[0];
            positionText.Text = mainManufacturer.Position;
            positionText.IsReadOnly = true;
            EmailText.Text = mainManufacturer.Email;
            PhoneText.Text = mainManufacturer.Phone;

            NameCompanyText.Text = mainManufacturer.NameCompany;
            DescriptionText.Text = mainManufacturer.Description;
            AddresText.Text = mainManufacturer.Address;
            RegionText.Text = mainManufacturer.Region;
            DirectorText.Text = mainManufacturer.PIBManager;
            PhoneDirectorText.Text = mainManufacturer.Manager_Phone;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (NameText.Text == "")
            {
                LogBar.Content = "Введіть ім'я!";
                LogBar.Visibility = Visibility.Visible;
                BName.Background = Brushes.Red;
            }
            else if (SurnameText.Text == "")
            {
                LogBar.Content = "Введіть прізвище!";
                LogBar.Visibility = Visibility.Visible;
                BSurname.Background = Brushes.Red;
            }
            else if (EmailText.Text == "")
            {
                LogBar.Content = "Введіть  дані пошти!";
                LogBar.Visibility = Visibility.Visible;
                BEmail.Background = Brushes.Red;
            }
            else if (PhoneText.Text == "")
            {
                LogBar.Content = "Введіть мобільний номер!";
                LogBar.Visibility = Visibility.Visible;
                BPhone.Background = Brushes.Red;
            }
            else if (NameCompanyText.Text == "")
            {
                LogBar.Content = "Введіть назву компанії!";
                LogBar.Visibility = Visibility.Visible;
                BCompanyName.Background = Brushes.Red;
            }
            else if (DescriptionText.Text == "")
            {
                LogBar.Content = "Введіть опис компанії!";
                LogBar.Visibility = Visibility.Visible;
                BDescription.Background = Brushes.Red;
            }
            else if (AddresText.Text == "")
            {
                LogBar.Content = "Введіть адресу компанії!";
                LogBar.Visibility = Visibility.Visible;
                BAddres.Background = Brushes.Red;
            }
            else if (RegionText.Text == "")
            {
                LogBar.Content = "Введіть область розташування компанії!";
                LogBar.Visibility = Visibility.Visible;
                BRegion.Background = Brushes.Red;
            }
            else if (DirectorText.Text == "")
            {
                LogBar.Content = "Введіть ініціали директора!";
                LogBar.Visibility = Visibility.Visible;
                BDirector.Background = Brushes.Red;
            }
            else if (PhoneDirectorText.Text == "")
            {
                LogBar.Content = "Введіть мобільний номер директора!";
                LogBar.Visibility = Visibility.Visible;
                BPhoneDir.Background = Brushes.Red;
            }
            else
            {
                ChangeData();
            }
         
        }
        private void ChangeData()
        {
            mainManufacturer.PIB = NameText.Text + " " + SurnameText.Text;
            mainManufacturer.Phone = PhoneText.Text;
            mainManufacturer.Email = EmailText.Text;

            mainManufacturer.Address = AddresText.Text;
            mainManufacturer.Region = RegionText.Text;
            mainManufacturer.PIBManager = DirectorText.Text;
            mainManufacturer.NameCompany = NameCompanyText.Text;
            mainManufacturer.Description = DescriptionText.Text;
            mainManufacturer.Manager_Phone = PhoneDirectorText.Text;

            try
            {
                DataBase.Get_Instance().Connect();
                MySqlCommand command = new MySqlCommand(
                            "UPDATE `user` SET `PIB` = @PIB, `Email` = @Email, `Mobile_number` = @Mobile_number " +
                            "WHERE `ID` = @ID", DataBase.Get_Instance().connection);
                command.Parameters.Add("@ID", MySqlDbType.Int32).Value = mainManufacturer.id;
                command.Parameters.Add("@PIB", MySqlDbType.VarChar).Value = mainManufacturer.PIB;
                command.Parameters.Add("@Email", MySqlDbType.VarChar).Value = mainManufacturer.Email;
                command.Parameters.Add("@Mobile_number", MySqlDbType.VarChar).Value = mainManufacturer.Phone;

                MySqlCommand command2 = new MySqlCommand(
                            "UPDATE `manufacturer` SET `Address` = @Address, `Region` = @Region, `Mobile_number` = @Mobile_number," +
                            " `Manager` = @Manager, `Name_company` = @NameCompany, `Description` = @Description " +
                            "WHERE `ID` = @ID", DataBase.Get_Instance().connection);
                command2.Parameters.Add("@ID", MySqlDbType.Int32).Value = mainManufacturer.ID_Manufacturer;
                command2.Parameters.Add("@Address", MySqlDbType.VarChar).Value = mainManufacturer.Address;
                command2.Parameters.Add("@Region", MySqlDbType.VarChar).Value = mainManufacturer.Region;
                command2.Parameters.Add("@Mobile_number", MySqlDbType.VarChar).Value = mainManufacturer.Phone;
                command2.Parameters.Add("@Manager", MySqlDbType.VarChar).Value = mainManufacturer.PIBManager;
                command2.Parameters.Add("@NameCompany", MySqlDbType.VarChar).Value = mainManufacturer.NameCompany;
                command2.Parameters.Add("@Description", MySqlDbType.VarChar).Value = mainManufacturer.Description;

                if (command.ExecuteNonQuery() > 0 && command2.ExecuteNonQuery()>0)
                {
                    LogBar.Foreground = Brushes.Green;
                    LogBar.Content = "Зміни успішно збережені!";
                    LogBar.Visibility = Visibility.Visible;
                }
                else
                {
                    LogBar.Foreground = Brushes.Red;
                    LogBar.Content = "Виникла помилка, зміни не були занесені!";
                    LogBar.Visibility = Visibility.Visible;
                }
            }
            catch (Exception e)
            {
                LogBar.Foreground = Brushes.Red;
                LogBar.Content = "Відсутнє з'єднання з БД!";
                LogBar.Visibility = Visibility.Visible;
                MessageBox.Show(e.Message);
            }
        }
        private void NameCompanyText_GotMouseCapture(object sender, MouseEventArgs e)
        {
            if(BCompanyName.Background == Brushes.Red) { BCompanyName.Background = new SolidColorBrush(Color.FromRgb(8, 17, 50)); LogBar.Visibility = Visibility.Hidden; }
        }

        private void DescriptionText_GotMouseCapture(object sender, MouseEventArgs e)
        {
            if (BDescription.Background == Brushes.Red) { BDescription.Background = new SolidColorBrush(Color.FromRgb(8, 17, 50)); LogBar.Visibility = Visibility.Hidden; }
        }

        private void AddresText_GotMouseCapture(object sender, MouseEventArgs e)
        {
            if (BAddres.Background == Brushes.Red) { BAddres.Background = new SolidColorBrush(Color.FromRgb(8, 17, 50)); LogBar.Visibility = Visibility.Hidden; }
        }

        private void RegionText_GotMouseCapture(object sender, MouseEventArgs e)
        {
            if (BRegion.Background == Brushes.Red) { BRegion.Background = new SolidColorBrush(Color.FromRgb(8, 17, 50)); LogBar.Visibility = Visibility.Hidden; }
        }

        private void DirectorText_GotMouseCapture(object sender, MouseEventArgs e)
        {
            if (BDirector.Background == Brushes.Red) { BDirector.Background = new SolidColorBrush(Color.FromRgb(8, 17, 50)); LogBar.Visibility = Visibility.Hidden; }
        }

        private void PhoneDirectorText_GotMouseCapture(object sender, MouseEventArgs e)
        {
            if (BPhone.Background == Brushes.Red) { BPhone.Background = new SolidColorBrush(Color.FromRgb(8, 17, 50)); LogBar.Visibility = Visibility.Hidden; }
        }

        private void NameText_GotMouseCapture(object sender, MouseEventArgs e)
        {
            if (BName.Background == Brushes.Red) { BName.Background = new SolidColorBrush(Color.FromRgb(8, 17, 50)); LogBar.Visibility = Visibility.Hidden; }
        }

        private void SurnameText_GotMouseCapture(object sender, MouseEventArgs e)
        {
            if (BSurname.Background == Brushes.Red) { BSurname.Background = new SolidColorBrush(Color.FromRgb(8, 17, 50)); LogBar.Visibility = Visibility.Hidden; }
        }

        private void positionText_GotMouseCapture(object sender, MouseEventArgs e)
        {
            if (BPosition.Background == Brushes.Red) { BPosition.Background = new SolidColorBrush(Color.FromRgb(8, 17, 50)); LogBar.Visibility = Visibility.Hidden; }
        }

        private void EmailText_GotMouseCapture(object sender, MouseEventArgs e)
        {
            if (BEmail.Background == Brushes.Red) { BEmail.Background = new SolidColorBrush(Color.FromRgb(8, 17, 50)); LogBar.Visibility = Visibility.Hidden; }
        }

        private void PhoneText_GotMouseCapture(object sender, MouseEventArgs e)
        {
            if (BPhone.Background == Brushes.Red) { BPhone.Background = new SolidColorBrush(Color.FromRgb(8, 17, 50)); LogBar.Visibility = Visibility.Hidden; }
        }
    }
}
