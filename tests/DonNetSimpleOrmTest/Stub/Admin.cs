using DotNetSimpleOrm.Model;

namespace DonNetSimpleOrmTest.Stub
{
    public class Admin : Model
    {
        public int Id { get; set; }
        
        public override string TableName()
        {
            return "admin";
        }
    }
}