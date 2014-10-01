using System.Web.Http.Results;
using FateDeck.Web.Controllers;
using FateDeck.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FateDeck.Tests.Controllers
{
    [TestClass]
    public class StandardEncounterControllerTest : ApiControllerTestBase<StandardEncounterController>
    {
        public StandardEncounterControllerTest() : base(new StandardEncounterController()) {}

        [TestMethod]
        public void Get()
        {
            // Act
            var result = Controller.Get() as OkNegotiatedContentResult<StandardEncounterViewModel>;

            // Assert
            result.IsNotNullOrEmpty();

            result.Content.Deployment.IsNotNullOrEmpty();
            result.Content.Schemes.IsNotNullOrEmpty();
            result.Content.Strategy.IsNotNullOrEmpty();

        }
    }
}