using FateDeck.Web.App_Start;
using FateDeck.Web.Controllers;
using FateDeck.Web.Repositories.Contracts;

namespace FateDeck.Tests
{
    public class ControllerTestBase<T> : TestBase where T : ControllerBase, new()
    {
        public T Controller { get; set; }
        public ControllerTestBase(T controller)
        {
            Controller = controller;
            Controller.Repositories = NinjectWebCommon.Get<IRepositoryFactory>();
        }
    }
}