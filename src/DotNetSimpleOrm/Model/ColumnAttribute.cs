using System;

namespace DotNetSimpleOrm.Model
{
    //自定义注解类
    public class ColumnAttribute: Attribute
    {
        public string Name = null;

        public bool Primary = false;
    }
}