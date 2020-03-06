namespace DotNetSimpleOrm.Connector
{
    public interface IConnector
    {
        Entity FindOne(string sql);
    }
}