using System;
using System.Collections.Generic;
using System.Linq;
using ColumnAttribute = DotNetSimpleOrm.Model.ColumnAttribute;

namespace DotNetSimpleOrm.Query
{
    public class Query
    {
        private readonly Model.Model _model;

        private readonly List<WhereItem> _whereItems;

        public Query(Model.Model model)
        {
            _model = model;

            _whereItems = new List<WhereItem>();
        }

        public Query Where(string column, object value)
        {
            var whereItem = new WhereItem(column, "=", value);

            _whereItems.Add(whereItem);

            return this;
        }

        public Query WhereIn(string column, Array value)
        {
            _whereItems.Add(new WhereItem(column, "in", value));

            return this;
        }

        public Model.Model FindOne()
        {
            var entity = ConnectorManager.GetConnector().FindOne(getSql());
            if (entity == null)
            {
                return null;
            }

            return EntityToModel(entity);
        }

        private Model.Model EntityToModel (Entity entity)
        {
            var instance = (Model.Model)_model.GetType().Assembly.CreateInstance(_model.GetType().ToString());

            if (instance == null)
            {
                throw new Exception("the model instance could not be created: " + _model.GetType());
            }

            var tableName = instance.TableName();
            
            foreach (var propertyInfo in instance.GetType().GetProperties())
            {
                var attrs = propertyInfo.GetCustomAttributes(false);
                
                if (attrs.Length <= 0) continue;
                
                foreach (var attr in attrs)
                {
                    if (!(attr is ColumnAttribute column)) continue;
                    
                    var refColumnName = column.Name ?? Helper.ToSnakeCase(propertyInfo.Name);

                    var value = entity.GetValue(tableName, refColumnName);

                    propertyInfo.SetValue(instance, value);
                }
            }
            
            instance.AfterFillByDb();

            return instance;
        }
        
        public List<Model.Model> FindAll()
        {
            var entities = ConnectorManager.GetConnector().FindAll(getSql());

            return entities?.Select(EntityToModel).ToList();
        }

        private string getSql()
        {
            // todo gen
            return "select admin.*, test.* from admin left join test on test.admin_id = admin.id";
        }
    }
}