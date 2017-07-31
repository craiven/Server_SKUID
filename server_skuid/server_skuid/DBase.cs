using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;

using System.Data;
using System.Data.SqlClient;
using System.IO;




namespace GUI
{
    class DBase
    {

      
        public static void login(string login, string passw)
        {
            string connStr = "server=" + Properties.Settings.Default.IP + ";user=" + Properties.Settings.Default.DBuser + ";database = " + Properties.Settings.Default.DBname + ";password=" + Properties.Settings.Default.DBpasswd + ";";
            //database = " + Properties.Settings.Default.DBname + ";
            //user CurrentUser = new user();
            string sql = "select * from "+ Properties.Settings.Default.DBname + ".auth where name ='"+login+"';";
            Console.WriteLine(sql);

            // создаём объект для подключения к БД
            MySqlConnection conn = new MySqlConnection(connStr);
            // объект для выполнения SQL-запроса
            MySqlCommand command = new MySqlCommand(sql, conn);

            

            // устанавливаем соединение с БД
            try
            {
                conn.Open();
                user.set();
                // читаем результат
                // объект для чтения ответа сервера
                MySqlDataReader reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    
                    user.Name = reader["name"].ToString();

                    if (passw == reader["passwd"].ToString())
                    {
                        
                        //user.Name = reader["name"].ToString();
                        user.Password = reader["passwd"].ToString();
                        user.Privilegies = reader["privilegies"].ToString();
                        user.login = true;
                        //Console.Write("Добро пожаловать "+ user.Name);
                    
                    }

                }
                
                reader.Close(); // закрываем reader
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);

            }

            finally
            {

            conn.Close(); // закрываем соединение
            }

          }

        public static void Create()
        {
            string connStr = "server=" + Properties.Settings.Default.IP + ";user=" + Properties.Settings.Default.DBuser + ";password=" + Properties.Settings.Default.DBpasswd + ";";
            //database = " + Properties.Settings.Default.DBname + ";
            string sql = "CREATE DATABASE  IF NOT EXISTS `"+Properties.Settings.Default.DBname+"` /*!40100 DEFAULT CHARACTER SET latin1 */;";
            sql += "USE `"+Properties.Settings.Default.DBname+"`;";
            sql += File.ReadAllText(@"schema\schema.sql");
            Console.Write(sql);

            if (sql == null)
            {
                sql = "SELECT info FROM base_info";
            }

            // создаём объект для подключения к БД
            MySqlConnection conn = new MySqlConnection(connStr);
            // объект для выполнения SQL-запроса
            MySqlCommand command = new MySqlCommand(sql, conn);

            // устанавливаем соединение с БД
            try
            {
                conn.Open();
                // читаем результат
                // объект для чтения ответа сервера
                command.ExecuteNonQuery();
          
                
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            finally
            {
                conn.Close(); // закрываем соединение
            }

        }
        
       public static string DBConect()
        {
            // строка подключения к БД
            
            string connStr = "server=" + Properties.Settings.Default.IP + ";user=" + Properties.Settings.Default.DBuser + ";database = " + Properties.Settings.Default.DBname + ";password=" + Properties.Settings.Default.DBpasswd + ";";
            //database = " + Properties.Settings.Default.DBname + ";

            // создаём объект для подключения к БД
            MySqlConnection conn = new MySqlConnection(connStr);
            // устанавливаем соединение с БД
            
            try
            {
                conn.Open();
                // запрос
                string sql = "SELECT info FROM base_info";
                // объект для выполнения SQL-запроса
                MySqlCommand command = new MySqlCommand(sql, conn);
                // объект для чтения ответа сервера
                MySqlDataReader reader = command.ExecuteReader();
                // читаем результат
               
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.ErrorCode.ToString());
                return ex.ErrorCode.ToString();

            }
            
            finally
            {
                 conn.Close(); // закрываем соединение
           }

            return "0";
            
        }



        public static DataTable DBQuery(string sql)
        {
            string connStr = "server=" + Properties.Settings.Default.IP + ";user=" + Properties.Settings.Default.DBuser + ";database = " + Properties.Settings.Default.DBname + ";password=" + Properties.Settings.Default.DBpasswd + ";";
            //database = " + Properties.Settings.Default.DBname + ";
            DataTable dt = new DataTable();

            if (sql == null)
            {
                sql = "SELECT info FROM base_info";
            }

            // создаём объект для подключения к БД
            MySqlConnection conn = new MySqlConnection(connStr);
            // объект для выполнения SQL-запроса
            MySqlCommand command = new MySqlCommand(sql, conn);
            
            // устанавливаем соединение с БД
            try
            {
                conn.Open();
                // читаем результат
                // объект для чтения ответа сервера
                
                dt.Load(command.ExecuteReader()); 
                //dt = reader.GetSchemaTable("Tables");
                  
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                
            }

            finally
            {
                
                conn.Close(); // закрываем соединение
                
            }

            return dt;
        }



    }
}
