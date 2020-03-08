using System.Collections.Generic;

namespace DotNetSimpleOrm.Connector
{
    public interface IConnector
    {
        Entity FindOne (string sql);

        IEnumerable<Entity> FindAll (string sql);

        long Insert (Model.Model model);
    }
}