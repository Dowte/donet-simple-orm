namespace DotNetSimpleOrm.Query
{
    public class WhereItem
    {
        public WhereItem (string column, string operatorStr, object value)
        {
            Value = value;
            Column = column;
            OperatorStr = operatorStr;
        }
        
        public string Column { get; set; }
        
        public string OperatorStr { get; set; }
        
        public object Value { get; set; }
    }
}