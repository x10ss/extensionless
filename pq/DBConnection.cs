using MySql.Data.MySqlClient;

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
       private static MySqlConnection connection= new MySqlConnection("Database=lovrenco_x10sionless;Server=49.12.60.2;Username=lovrenco_x10sionless2020; pwd=diegoRecoba8*;");

        public static MySqlConnection Connection
        {
            get
            {
                MySqlConnection connection = new MySqlConnection("Database=lovrenco_x10sionless;Server=49.12.60.2;Username=lovrenco_x10sionless2020; pwd=diegoRecoba8*;");
                connection.Open();
                return connection;
            }
            
        }

      

        public static bool IsConnect()
        {
            if (Connection == null)
            {

                connection = new MySqlConnection("Database=lovrenco_x10sionless;Server=htttps://www.extensionless.com;Username=lovrenco_x10sionless2020; pwd=diegoRecoba8*;");



            }
            return true;
        }
        public static void Close()
        {
            connection = new MySqlConnection("Database=lovrenco_x10sionless;Server=htttps://www.extensionless.com;Username=lovrenco_x10sionless2020; pwd=diegoRecoba8*;");

            connection.Close();
            connection = null;
        }

    }
}
