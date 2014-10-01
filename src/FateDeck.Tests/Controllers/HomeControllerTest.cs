using System.Web.Http;
using System.Web.Mvc;
using FateDeck.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FateDeck.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest:ControllerTestBase<HomeController>
    {
        public HomeControllerTest() : base(new HomeController()) {}

        [TestMethod]
        public void Index()
        {
            // Act
            var result = Controller.Index() as ViewResult;

            // Assert
            result.IsNotNullOrEmpty();
        }

        [TestMethod]
        public void About()
        {
            // Act
            var result = Controller.About() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
