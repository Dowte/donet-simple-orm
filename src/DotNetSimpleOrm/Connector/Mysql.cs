using MySql.Data.MySqlClient;

namespace DotNetSimpleOrm.Connector
{
    public class Mysql
    {
        public static MySqlConnection Connect (string connectString )
        {
            return new MySqlConnection(connectString);
        }
    }
}