using Client_MilkAndMeat.DB;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Client_MilkAndMeat.Users
{
    public enum UserType
    {
        Unregistered,
        Reseller,
        Manufacture,
        Moderator
    }
    public class User
    {
        public uint id;
        public UserType userType;
        public string PIB;
        public string Email;
        public string Phone;
        public int UserRegistration(string Login, string Password, string NS, string Email, string Phone, UserType userType)
        {
            try
            {
                DataBase.Get_Instance().Connect();
                MySqlCommand command = new MySqlCommand(
                            "INSERT INTO `user` (`Type`, `PIB`, `Email`, `Mobile_number`, `Login`, `Password`) " +
                            "VALUES ( @Type, @PIB, @Email, @Mobile_number, @Login, @Password)", DataBase.Get_Instance().connection);

                command.Parameters.Add("@Type", MySqlDbType.VarChar).Value = userType;
                command.Parameters.Add("@PIB", MySqlDbType.VarChar).Value = NS;
                command.Parameters.Add("@Email", MySqlDbType.VarChar).Value = Email;
                command.Parameters.Add("@Mobile_number", MySqlDbType.VarChar).Value = Phone;
                command.Parameters.Add("@Login", MySqlDbType.VarChar).Value = Login;
                command.Parameters.Add("@Password", MySqlDbType.VarChar).Value = Password;

                if (command.ExecuteNonQuery() > 0)
                {
                    this.Email = Email; this.Phone = Phone; this.PIB = NS; this.userType = userType;
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
        public void Set_UserType(UserType userType)
        {
            this.userType = userType;
        }
        public int UserLogin(string login, string password)
        {
            DataTable temp = new DataTable();
            try
            {
                DataBase.Get_Instance().Connect();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `user` WHERE `Login` = @login AND `Password` = @password", DataBase.Get_Instance().connection);
                command.Parameters.Add("@login", MySqlDbType.VarChar).Value = login;
                command.Parameters.Add("@password", MySqlDbType.VarChar).Value = password;
                adapter.SelectCommand = command;

                adapter.Fill(temp);
                if (temp.Rows.Count > 0)
                {
                    this.id = Convert.ToUInt32(temp.Rows[0][0]);
                    this.userType = ConvertToEnum(Convert.ToString(temp.Rows[0][1]));
                    this.PIB = Convert.ToString(temp.Rows[0][2]);
                    this.Email = Convert.ToString(temp.Rows[0][3]);
                    this.Phone = Convert.ToString(temp.Rows[0][4]);
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
        public User(uint id, UserType userType, string PIB, string Email, string Phone)
        {
            this.id = id;
            this.userType = userType;
            this.PIB = PIB;
            this.Email = Email;
            this.Phone = Phone;
        }
        public User(uint id, UserType userType, string PIB)
        {
            this.id = id;
            this.userType = userType;
            this.PIB = PIB;
            this.Email = "";
            this.Phone = "";
        }
        public User()
        {
            this.id = 0;
            this.userType = UserType.Unregistered;
            this.PIB = "";
            this.Email = "";
            this.Phone = "";
        }
        public User(User user)
        {
            this.id = user.id;
            this.userType = user.userType;
            this.PIB = user.PIB;
            this.Email = user.Email;
            this.Phone = user.Phone;
        }
        public UserType GetUserType() { return this.userType; }
        public static UserType ConvertToEnum(string UEnum)
        {
            switch (UEnum)
            {
                case "Reseller":
                    {
                        return UserType.Reseller;
                    }
                case "Manufacture":
                    {
                        return UserType.Manufacture;
                    }
                case "Moderator":
                    {
                        return UserType.Moderator;
                    }
                default: return UserType.Unregistered;
            }
        }
    }
}
