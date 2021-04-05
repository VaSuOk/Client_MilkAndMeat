using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client_MilkAndMeat.DB
{
    class DataBase
    {
        public MySqlConnection connection;
         
        private static DataBase dataBase;
        private DataBase()
        {
            connection = new MySqlConnection("server=localhost; port=3306; username=root; password=; database=meatandmilk;");
        }
        public static DataBase Get_Instance() { return dataBase == null ? dataBase = new DataBase() : dataBase; }
        public void Connect()
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                else Console.WriteLine("БД не знайдена");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void Disconnect()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
        public bool Is_Connected()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                return true;
            }
            return false;
        }
    }
}
