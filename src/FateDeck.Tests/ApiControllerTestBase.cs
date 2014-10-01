using FateDeck.Web.App_Start;
using FateDeck.Web.Controllers;
using FateDeck.Web.Repositories.Contracts;

namespace FateDeck.Tests
{
    public class ApiControllerTestBase<T> : TestBase where T : ApiControllerBase, new()
    {
        public T Controller { get; set; }
        public ApiControllerTestBase(T controller)
        {
            Controller = controller;
            Controller.Repositories = NinjectWebCommon.Get<IRepositoryFactory>();
        }
    }
}