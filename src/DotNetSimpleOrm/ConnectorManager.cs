using DotNetSimpleOrm.Connector;

namespace DotNetSimpleOrm
{
    public static class ConnectorManager
    {
        private static IConnector _connector;

        public static void Register(IConnector connector)
        {
            _connector = connector;
        }

        public static IConnector GetConnector()
        {
            return _connector;
        }
    }
}