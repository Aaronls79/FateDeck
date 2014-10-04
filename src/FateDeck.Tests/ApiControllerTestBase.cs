using FateDeck.Web.Controllers;
using FateDeck.Web.Repositories;

namespace FateDeck.Tests
{
    public class ApiControllerTestBase<T> : TestBase where T : ApiControllerBase, new()
    {
        public T Controller { get; set; }
        public ApiControllerTestBase(T controller)
        {
            Controller = controller;
            Controller.Repositories = new RepositoryFactory();
        }
    }
}