using Client_MilkAndMeat.Users;
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
            EmailText.Text = mainManufacturer.Email;
            PhoneText.Text = mainManufacturer.Phone;

            NameCompanyText.Text = mainManufacturer.NameCompany;
            DescriptionText.Text = mainManufacturer.Description;
            AddresText.Text = mainManufacturer.Address;
            RegionText.Text = mainManufacturer.Region;
            DirectorText.Text = mainManufacturer.PIBManager;
            PhoneDirectorText.Text = mainManufacturer.Manager_Phone;
        }
    }
}
