using System.Collections.Generic;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;

namespace DotNetSimpleOrm.Connector.Mysql
{
    public class Mysql: IConnector
    {
        private readonly string _connectString;

        public Mysql (string connectString)
        {
            _connectString = connectString;
        }
        
        private MySqlConnection _getConnection()
        {
            var mySqlConnection = new MySqlConnection(_connectString);
            mySqlConnection.Open();

            return mySqlConnection;
        }

        public Entity FindOne(Query.Query query)
        {
            // todo 
            string sql = "";
            var com = new MySqlCommand(sql, _getConnection());

            var mySqlDataReader = com.ExecuteReader();
            var dataRowCollection = mySqlDataReader?.GetSchemaTable()?.Rows;

            if (dataRowCollection == null) return null;
            var tableMap = (from DataRow row in dataRowCollection select row["BaseTableName"].ToString()).ToList();

            var entity = new Entity();
            while (mySqlDataReader.Read())
            {
                for (var i = 0; i < mySqlDataReader.FieldCount; i++)
                {
                    entity.Add(tableMap[i], mySqlDataReader.GetName(i), mySqlDataReader[mySqlDataReader.GetName(i)]);
                }
            
                break;
            }
            
            mySqlDataReader.Close();
                
            return entity.Dirtied ? entity : null;
        }

        public IEnumerable<Entity> FindAll(Query.Query query)
        {
            // todo 
            string sql = "";
            var com = new MySqlCommand(sql, _getConnection());
            
            var mySqlDataReader = com.ExecuteReader();

            var dataRowCollection = mySqlDataReader?.GetSchemaTable()?.Rows;

            if (dataRowCollection == null) return null;
            var tableMap = (from DataRow row in dataRowCollection select row["BaseTableName"].ToString()).ToList();

            var entities = new List<Entity>();

            while (mySqlDataReader.Read())
            {
                var entity = new Entity();
                for (var i = 0; i < mySqlDataReader.FieldCount; i++)
                {
                    entity.Add(tableMap[i], mySqlDataReader.GetName(i), mySqlDataReader[mySqlDataReader.GetName(i)]);
                }
                
                entities.Add(entity);
            }
            
            mySqlDataReader.Close();

            return entities.Count > 0 ? entities : null;
        }

        public long Insert(Model.Model model)
        {
            var cmd = new MySqlCommand {Connection = _getConnection()};
            var attributes = model.GetAttributes();

            cmd.CommandText = "INSERT INTO " + model.TableName() + " VALUES(@" +
                              string.Join(",@", attributes.Keys) + ")";

            cmd.Prepare();

            foreach (var (key, value) in attributes)
            {
                cmd.Parameters.AddWithValue("@" + key, value);
            }

            cmd.ExecuteNonQuery();

            model.GetPrimaryColumnProperty().SetValue(model, cmd.LastInsertedId);
            
            return cmd.LastInsertedId;
        }

        public long Update(Model.Model model)
        {
            var attributes = model.GetAttributes();
            var oldAttributes = model.OriginAttributes;


            var changedAttributes = new Dictionary<string, object>();
            
            foreach (var (columnName, value) in attributes)
            {
                if (!value.Equals(oldAttributes[columnName]))
                {
                    changedAttributes.Add(columnName, value);
                }
            }
            
            var primaryColumnName = model.GetPrimaryColumnName();
            var cmd = new MySqlCommand {Connection = _getConnection()};

            var sql = "UPDATE " + model.TableName() + " SET";

            cmd.CommandText = changedAttributes.Keys.Aggregate(sql, (current, attributesKey) => current + " " + attributesKey + "=@" + attributesKey);
            cmd.CommandText += " where " + primaryColumnName + "=@_primaryValue";
            
            foreach (var (key, value) in changedAttributes)
            {
                cmd.Parameters.AddWithValue("@" + key, value);
            }
            
            cmd.Parameters.AddWithValue("_primaryValue", oldAttributes[primaryColumnName]);
  
            cmd.ExecuteNonQuery();

            return cmd.LastInsertedId;
        }
    }
}