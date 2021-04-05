using Client_MilkAndMeat.DB;
using Client_MilkAndMeat.UserControls;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Client_MilkAndMeat.Users
{
    public class Manufacturer  :  User
    {
        public UInt16 ID_Manufacturer;
        public string Position;
        public string Address;
        public string Region;
        public string Manager_Phone;
        public string PIBManager;
        public string NameCompany;
        public string Description;

        public UInt16 Get_IDManufactorer() { return this.ID_Manufacturer; }
        public Manufacturer(User user) : base(user)
        {
            
        }
        public int InitManufacturer()
        {
            DataTable temp = new DataTable();
            try
            {
                DataBase.Get_Instance().Connect();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `manufacturer` WHERE `ID_User` = @ID_User", DataBase.Get_Instance().connection);
                command.Parameters.Add("@ID_User", MySqlDbType.Int32).Value = id;
                adapter.SelectCommand = command;
                adapter.Fill(temp);
                if(temp.Rows.Count > 0)
                {
                    this.ID_Manufacturer = Convert.ToUInt16(temp.Rows[0][0]);
                    this.Position = Convert.ToString(temp.Rows[0][2]);
                    this.Address = Convert.ToString(temp.Rows[0][3]);
                    this.Region = Convert.ToString(temp.Rows[0][4]);
                    this.Manager_Phone = Convert.ToString(temp.Rows[0][5]);
                    this.PIBManager = Convert.ToString(temp.Rows[0][6]);
                    this.NameCompany = Convert.ToString(temp.Rows[0][7]);
                    this.Description = Convert.ToString(temp.Rows[0][8]);


                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return -1;
            }
            
        }
    }
}
