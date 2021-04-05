using Client_MilkAndMeat.DB;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Client_MilkAndMeat.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ControlMyProducts.xaml
    /// </summary>
    public partial class ControlMyProducts : UserControl
    {
        public List<Product> MyProducts { get; set; }

        public ControlMyProducts(int id)
        {
            InitializeComponent();
            MyProducts = new List<Product>();
            Init_Products(id);
            if (MyProducts.Count > 0)
                ListViewProducts.ItemsSource = MyProducts;
        }
        private int Init_Products(int id)
        {
            DataTable temp = new DataTable();
            try
            {
                DataBase.Get_Instance().Connect();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `product` WHERE `ID_manufacturer` = @ID_manufacturer", DataBase.Get_Instance().connection);
                command.Parameters.Add("@ID_manufacturer", MySqlDbType.Int32).Value = id;
                adapter.SelectCommand = command;
                 
                adapter.Fill(temp);
                if (temp.Rows.Count > 0)
                {
                    for (int i = 0; i < temp.Rows.Count; i++)
                    {
                        Product product = new Product();
                        product.ID = Convert.ToUInt32(temp.Rows[i][0]);
                        product.ID_manufacture = Convert.ToUInt32(temp.Rows[i][1]);
                        product.Name = Convert.ToString(temp.Rows[i][2]);
                        product.Description = Convert.ToString(temp.Rows[i][3]);
                        product.Sort  = Convert.ToString(temp.Rows[i][4]);
                        product.Groupe = Convert.ToString(temp.Rows[i][5]);

                        byte[] arrimage = (byte[]) temp.Rows[i][6];
                        var imageSource = new BitmapImage();
                        MemoryStream ms = new System.IO.MemoryStream(arrimage);
                        imageSource.BeginInit();
                        imageSource.StreamSource = ms;
                        imageSource.CacheOption = BitmapCacheOption.OnLoad;
                        imageSource.EndInit();

                        
                        product.image = imageSource;

                        product.Price = Convert.ToDouble(temp.Rows[i][9]);
                        MyProducts.Add(product);
                    }
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return -1;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Product product = ((Button)sender).Tag as Product;
        }

    }
}
