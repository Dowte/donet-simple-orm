using System.Collections.Generic;

namespace DotNetSimpleOrm.Connector
{
    public interface IConnector
    {
        Entity FindOne (Query.Query query);

        IEnumerable<Entity> FindAll (Query.Query query);

        long Insert (Model.Model model);
        
        long Update (Model.Model model);
    }
}