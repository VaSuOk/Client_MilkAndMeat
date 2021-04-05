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


                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                    img.SetValue(Image.SourceProperty, isc.ConvertFromString(imageName));
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
