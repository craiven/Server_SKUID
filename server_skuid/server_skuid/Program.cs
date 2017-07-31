using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;


namespace server_skuid
{
    class Program
    {
        public struct user
        {
            public static string Name;
            public static string Password;
            public static string Privilegies;
            public static bool login;

            public static void set()
            {
                user.Name = "guest";
                user.Password = "";
                user.Privilegies = "0";
                user.login = false;

            }
        }

            static void Main(string[] args)
        {
            while(true)
            {
                try
                {
                    connect.Listen();

                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {

                }
            }
        }
    }
}
