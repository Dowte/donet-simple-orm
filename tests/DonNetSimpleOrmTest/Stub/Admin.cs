using DotNetSimpleOrm.Model;

namespace DonNetSimpleOrmTest.Stub
{
    public class Admin : Model
    {
        [Column]
        public int Id { get; set; }
        
        [Column(Name = "user_name")]
        public string UserName { get; set; }
        
        public override string TableName()
        {
            return "admin";
        }
    }
}