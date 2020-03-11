using System;

namespace DotNetSimpleOrm.Query
{
    public class WhereItem
    {
        public string FormattdValue;
        
        public WhereItem (string column, string operatorStr, object value)
        {
            Value = value;
            Column = column;
            OperatorStr = operatorStr;

            FormattdValue = parseValue();
        }
        
        public string Column { get; }
        
        public string OperatorStr { get; }
        
        public object Value { get; }

        public string parseValue()
        {
            switch (Column)
            {
                case "=" :
                    return Value.ToString();
                case "in":
                    if (Value is Array)
                    {
                        return "('" + string.Join("','", Value) + "')";
                    }
                    else
                    {
                        throw new Exception("the in operator only support array value");
                    }

            }
            
            throw new Exception("not support operator: " + OperatorStr);
        }
    }
}