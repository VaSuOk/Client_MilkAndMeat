using Client_MilkAndMeat.UserControls;
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
using System.Windows.Shapes;

namespace Client_MilkAndMeat
{
    /// <summary>
    /// Логика взаимодействия для MainManufactore.xaml
    /// </summary>
    public partial class MainManufactore : Window
    {
        public Manufacturer manufacturer;
        public MainManufactore(User user)
        {
            InitializeComponent();
            manufacturer = new Manufacturer(user);
            
            UserControl usc = null;
            GridMain.Children.Clear();

            if (manufacturer.InitManufacturer() == 1)
            {
                usc = new UserProfileControl(ref manufacturer);
                GridMain.Children.Add(usc);
            }
            else
            {

                usc = new UserProfileControl(ref manufacturer);
                GridMain.Children.Add(usc);
                //init control init data
            }
        }
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }
        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl usc = null;
            GridMain.Children.Clear();

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "MyProduct":
                    usc = new ControlMyProducts(manufacturer.ID_Manufacturer);
                    GridMain.Children.Add(usc);
                    break;
                case "Account":
                    usc = new UserProfileControl(ref manufacturer);
                    GridMain.Children.Add(usc);
                    break;
                case "CreateProduct":
                    usc = new AddProduct(manufacturer.ID_Manufacturer);
                    GridMain.Children.Add(usc);
                    break;
                    
                default:
                    break;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) DragMove();
        }
    }
}
