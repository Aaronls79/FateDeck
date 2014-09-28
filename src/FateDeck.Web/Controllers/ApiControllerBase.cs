using System.Web.Http;
using FateDeck.Web.Repositories.Contracts;
using Ninject;

namespace FateDeck.Web.Controllers
{
    public class ApiControllerBase : ApiController
    {
        [Inject]
        public IRepositoryFactory Repositories { get; set; }
    }
}