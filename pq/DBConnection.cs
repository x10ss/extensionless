using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Windows;

namespace pq
{
    public class DBConnection
    {
        private static DBConnection _instance = null;
        public static DBConnection Instance()
        {
            if (_instance == null)
                _instance = new DBConnection();
            return _instance;
        }

        private static string databaseName = string.Empty;
        public static string DatabaseName
        {
            get { return databaseName; }
            set { databaseName = value; }
        }

        public static string Password { get; set; }
       private static MySqlConnection connection= new MySqlConnection("Database=; Server=extensionless.com; Username=; pwd=;");

        public static MySqlConnection Connection
        {
            get
            {
                MySqlConnection connection = new MySqlConnection("Database=; Server=extensionless.com; Username=; pwd=;");
                connection.Open();
                return connection;
            }
            
        }

      

        public static bool IsConnect()
        {
            if (Connection == null)
            {

                connection = new MySqlConnection("Database=; Server=extensionless.com; Username=; pwd=;");



            }
            return true;
        }
        public static void Close()
        {
            connection = new MySqlConnection("Database=; Server=extensionless.com; Username=; pwd=;") ;

            connection.Close();
            connection = null;
        }

    }
}
