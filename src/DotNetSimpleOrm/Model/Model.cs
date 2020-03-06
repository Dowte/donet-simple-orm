namespace DotNetSimpleOrm.Model
{
    public abstract class Model
    {
        public Query.Query Query()
        {
            return new Query.Query(this);
        }

        public static Query.Query Query<T>()
        {
            return new Query.Query((Model)typeof(T).Assembly.CreateInstance(typeof(T).ToString()));
        }

        public abstract string TableName();
    }
}