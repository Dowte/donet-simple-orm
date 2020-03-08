using DonNetSimpleOrmTest.Stub;
using DotNetSimpleOrm;
using DotNetSimpleOrm.Connector;
using DotNetSimpleOrm.Model;
using NUnit.Framework;

namespace DonNetSimpleOrmTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            ConnectorManager.Register(new Mysql("server=127.0.0.1;user id=root;password=123456;database=test; pooling=true;"));
            
            var admin = new Admin();
            
            admin.Query().Where("id", 1).FindOne();

            Assert.AreEqual(1, admin.Id);


            Admin admin2 = (Admin)Model.Query<Admin>().Where("id", 1).FindOne();
            Assert.AreEqual(1, admin2.Id);
        }
        
        [Test]
        public void Test2()
        {
            ConnectorManager.Register(new Mysql("server=127.0.0.1;user id=root;password=123456;database=test; pooling=true;"));

            var admin = new Admin {UserName = "test_name"};
            admin.Save();
        }
    }
}