using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Client_MilkAndMeat
{
    public enum UserType
    {
        Unregistered,
        Reseller,
        Manufacture,
        Moderator
    }
    public static class RequestToServer
    {
        private const int port = 8888;
        private const string address = "127.0.0.1";

        public static short SendData(string Data)
        {
            
            TcpClient client = null;
            try
            {
                client = new TcpClient(address, port);
                NetworkStream stream = client.GetStream();
                
                while (true)
                {
                    byte[] data = Encoding.Unicode.GetBytes(Data);
                    stream.Write(data, 0, data.Length);

                    data = new byte[64]; 
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);
                    return Convert.ToInt16(builder.ToString()); 
                }
            }
            catch
            {
                //запис в логи
                return -1;
            }
            finally
            {
                if(client != null) client.Close();
            }
        }
    }
}
