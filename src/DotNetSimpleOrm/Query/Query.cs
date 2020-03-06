using System;
using System.Collections.Generic;

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

            var tableName = _model.TableName();
            foreach (var propertyInfo in _model.GetType().GetProperties())
            {
                var refColumnName = Helper.ToSnakeCase(propertyInfo.Name);

                var value = entity.GetValue(tableName, refColumnName);

                propertyInfo.SetValue(_model, value);
            }

            return _model;
        }

        private string getSql()
        {
            // todo gen
            return "select admin.*, test.* from admin left join test on test.admin_id = admin.id";
        }
    }
}