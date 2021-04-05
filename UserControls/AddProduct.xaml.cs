using Client_MilkAndMeat.DB;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для AddProduct.xaml
    /// </summary>
    public partial class AddProduct : UserControl
    {
        string  imageName, strName;
        int ID_man;
        public AddProduct(int id_man)
        {
            ID_man = id_man;
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (NameProductText.Text == "" || NameProductText.Text == "Input name")
            {
                LogBar.Content = "Введіть назву продукту!";
                LogBar.Visibility = Visibility.Visible;
                BName.Background = Brushes.Red;
            }
            else if (DescriptionText.Text == "" || DescriptionText.Text == "Input Description")
            {
                LogBar.Content = "Введіть опис продукту!";
                LogBar.Visibility = Visibility.Visible;
                BDescription.Background = Brushes.Red;
            }
            else if (SortText.Text == "" || SortText.Text == "input sort")
            {
                LogBar.Content = "Введіть сорт продукту!";
                LogBar.Visibility = Visibility.Visible;
                BSort.Background = Brushes.Red;
            }
            else if (GroupText.Text == "" || GroupText.Text == "Input group")
            {
                LogBar.Content = "Введіть групу продукту!";
                LogBar.Visibility = Visibility.Visible;
                BGroup.Background = Brushes.Red;
            }
            else if (imageName == "" || imageName == null)
            {
                LogBar.Content = "Оберіть фотографію продукту!";
                LogBar.Visibility = Visibility.Visible;
                BImage.Background = Brushes.Red;
            }
            else if (PriceText.Text == "" || PriceText.Text == "0")
            {
                LogBar.Content = "Введіть ціну продукту!";
                LogBar.Visibility = Visibility.Visible;
                BPrice.Background = Brushes.Red;
            }
            else
            {
                try
                {
                    if (imageName != "")
                    {
                        //Initialize a file stream to read the image file
                        FileStream fs = new FileStream(imageName, FileMode.Open, FileAccess.Read);

                        //Initialize a byte array with size of stream
                        byte[] imgByteArr = new byte[fs.Length];

                        //Read data from the file stream and put into the byte array
                        fs.Read(imgByteArr, 0, Convert.ToInt32(fs.Length));

                        //Close a file stream
                        fs.Close();

                        DataBase.Get_Instance().Connect();
                        MySqlCommand command = new MySqlCommand(
                                    "INSERT INTO `product` (`ID_manufacturer`, `Name`, `Description`, `Sort`, `Groupe`, `Image`, `Price`) " +
                                    "VALUES ( @ID_manufacturer, @Name, @Description, @Sort, @Groupe, @Image, @Price)", DataBase.Get_Instance().connection);

                        command.Parameters.Add("@ID_manufacturer", MySqlDbType.Int32).Value = ID_man;
                        command.Parameters.Add("@Name", MySqlDbType.VarChar).Value = NameProductText.Text;
                        command.Parameters.Add("@Description", MySqlDbType.VarChar).Value = DescriptionText.Text;
                        command.Parameters.Add("@Sort", MySqlDbType.VarChar).Value = SortText.Text;
                        command.Parameters.Add("@Groupe", MySqlDbType.VarChar).Value = GroupText.Text;
                        command.Parameters.Add("@Image", MySqlDbType.MediumBlob).Value = imgByteArr;
                        command.Parameters.Add("@Price", MySqlDbType.Double).Value = Convert.ToDouble(PriceText.Text);

                        if (command.ExecuteNonQuery() > 0)
                        {
                            LogBar.Content = "Дані успішно занесені !";
                            LogBar.Foreground = Brushes.Green;
                            LogBar.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            LogBar.Content = "Щось пішло не так, дані не були занесені !";
                            LogBar.Foreground = Brushes.Red;
                            LogBar.Visibility = Visibility.Visible;
                        }


                    }
                }
                catch (Exception ex)
                {
                    LogBar.Content = "Відсутнє з'єднання з БД, дані не були занесені !";
                    LogBar.Foreground = Brushes.Red;
                    LogBar.Visibility = Visibility.Visible;
                    //MessageBox.Show(ex.Message);
                }
            }
        }

        private void PriceText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789,".IndexOf(e.Text) < 0;
        }

        private void NameProductText_GotMouseCapture(object sender, MouseEventArgs e)
        {
            if (BName.Background == Brushes.Red) { BName.Background = new SolidColorBrush(Color.FromRgb(8, 17, 50)); LogBar.Visibility = Visibility.Hidden; }
        }

        private void DescriptionText_GotMouseCapture(object sender, MouseEventArgs e)
        {
            if (BDescription.Background == Brushes.Red) { BDescription.Background = new SolidColorBrush(Color.FromRgb(8, 17, 50)); LogBar.Visibility = Visibility.Hidden; }
        }

        private void SortText_GotMouseCapture(object sender, MouseEventArgs e)
        {
            if (BSort.Background == Brushes.Red) { BSort.Background = new SolidColorBrush(Color.FromRgb(8, 17, 50)); LogBar.Visibility = Visibility.Hidden; }
        }

        private void GroupText_GotMouseCapture(object sender, MouseEventArgs e)
        {
            if (BGroup.Background == Brushes.Red) { BGroup.Background = new SolidColorBrush(Color.FromRgb(8, 17, 50)); LogBar.Visibility = Visibility.Hidden; }
        }

        private void Button_GotMouseCapture(object sender, MouseEventArgs e)
        {
            if (BImage.Background == Brushes.Red) { BImage.Background = new SolidColorBrush(Color.FromRgb(8, 17, 50)); LogBar.Visibility = Visibility.Hidden; }
        }

        private void PriceText_GotMouseCapture(object sender, MouseEventArgs e)
        {
            if (BPrice.Background == Brushes.Red) { BPrice.Background = new SolidColorBrush(Color.FromRgb(8, 17, 50)); LogBar.Visibility = Visibility.Hidden; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FileDialog fldlg = new OpenFileDialog();
                fldlg.InitialDirectory = Environment.SpecialFolder.MyPictures.ToString();
                fldlg.Filter = "Image File (*.jpg;*.bmp;*.gif)|*.jpg;*.bmp;*.gif";
                fldlg.ShowDialog();
                {
                    strName = fldlg.SafeFileName;
                    imageName = fldlg.FileName;
                    ImageSourceConverter isc = new ImageSourceConverter();
                    ImageBrush imgbr = new ImageBrush();
                    imgbr.ImageSource = new BitmapImage(new Uri(imageName, UriKind.Absolute));
                    Bimg.Background = imgbr;
                    
                }
                fldlg = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

    }
}
