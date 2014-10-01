using System;
using System.IO;
using System.Linq;
using Dapper;
using FateDeck.Web.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FateDeck.Tests
{
    [TestClass]
    public class DataSourceTest : TestBase
    {
        [TestMethod]
        public void Create_WithNoDatabaseFile_CreatesNewDatabase()
        {
            File.Exists(DataSource.DbFile).ShouldEqual(true);
        }

        [TestMethod]
        public void Initialize_WithNoDatabaseFile_CreatesNewDatabaseAndInitializesDataSource()
        {
            File.Exists(DataSource.DbFile).ShouldEqual(true);
            using (var con = DataSource.Connection())
            {
                var deployment = con.Query<dynamic>(@"
                    SELECT * FROM Deployment
                ").FirstOrDefault();
                deployment.IsNotNullOrEmpty();
            }
        }
    }
}
