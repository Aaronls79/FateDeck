using System;
using System.IO;
using System.Linq;
using Dapper;
using FateDeck.Web.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FateDeck.Tests
{
    [TestClass]
    public class DataSourceTest
    {
        private readonly DataSource _dataSource = new DataSource();
        [TestInitialize]
        public void SetUp()
        {
            _dataSource.Delete();
        }

        [TestMethod]
        public void Create_WithNoDatabaseFile_CreatesNewDatabase()
        {
            _dataSource.Create();
            File.Exists(DataSource.DbFile).ShouldEqual(true);
        }

        [TestMethod]
        public void Initialize_WithNoDatabaseFile_CreatesNewDatabaseAndInitializesDataSource()
        {
            _dataSource.Initialize();
            File.Exists(DataSource.DbFile).ShouldEqual(true);
            using (var con = DataSource.Connection())
            {
                var deployment = con.Query<dynamic>(@"
                    SELECT * Deployment
                ").FirstOrDefault();
                deployment.IsNotNullOrEmpty();
                deployment.Name.IsNotNullOrEmpty();
            }
        }
    }
}
