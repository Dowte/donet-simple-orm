using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;

namespace DotNetSimpleOrm.Connector
{
    public class Mysql: IConnector
    {
        private readonly string _connectString;

        public Mysql (string connectString)
        {
            _connectString = connectString;
        }
        
        public MySqlConnection GetConnection ()
        {
            return new MySqlConnection(_connectString);
        }

        private MySqlConnection _getConnection()
        {
            return new MySqlConnection(_connectString);
        }

        public Entity FindOne(string sql)
        {
            MySqlConnection con = _getConnection();
            con.Open();
            MySqlCommand com = new MySqlCommand(sql, con);
            
            var mySqlDataReader = com.ExecuteReader();

            var dataRowCollection = mySqlDataReader?.GetSchemaTable()?.Rows;

            if (dataRowCollection == null) return null;
            var tableMap = (from DataRow row in dataRowCollection select row["BaseTableName"].ToString()).ToList();

            while (mySqlDataReader.Read())
            {
                var entity = new Entity();
                for (var i = 0; i < mySqlDataReader.FieldCount; i++)
                {
                    entity.Add(tableMap[i], mySqlDataReader.GetName(i), mySqlDataReader[mySqlDataReader.GetName(i)]);
                }
                return entity;
            }

            return null;
        }
    }
}