using FateDeck.Web.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FateDeck.Tests
{
    [TestClass]
    public class TestBase
    {
        private readonly DataSource _dataSource = new DataSource();
        public DataSource DataSource
        {
            get { return _dataSource; }
        }

        [TestInitialize]
        public void SetUp()
        {
            DataSource.Delete();
            DataSource.Create();
        }
    }
}