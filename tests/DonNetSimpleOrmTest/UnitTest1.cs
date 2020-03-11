using DonNetSimpleOrmTest.Stub;
using DotNetSimpleOrm;
using DotNetSimpleOrm.Connector;
using DotNetSimpleOrm.Connector.Mysql;
using DotNetSimpleOrm.Model;
using NUnit.Framework;

namespace DonNetSimpleOrmTest
{
    public class Tests
    {
        private const string connectString = "server=127.0.0.1;user id=root;password=123456;database=test; pooling=true;";
        
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            ConnectorManager.Register(new Mysql(connectString));
            
            Admin admin2 = (Admin)Model.Query<Admin>().Where("id", 1).FindOne();
            Assert.AreEqual(1, admin2.Id);
        }
        
        [Test]
        public void Test2()
        {
            ConnectorManager.Register(new Mysql(connectString));

            var admin = new Admin {UserName = "test_name"};
            admin.Save();
        }
        
        [Test]
        public void Test3()
        {
            ConnectorManager.Register(new Mysql(connectString));

            var admin = (Admin)Model.Query<Admin>().Where("id", 1).FindOne();

            admin.UserName = "test_update";
            admin.Save();
            
            Assert.AreEqual("test_update", admin.UserName);
            
            var admin2 = (Admin)Model.Query<Admin>().Where("id", 1).FindOne();

            Assert.AreEqual("test_update", admin2.UserName);
        }
    }
}