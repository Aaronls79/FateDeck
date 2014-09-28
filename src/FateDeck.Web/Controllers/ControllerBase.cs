using System.Web.Mvc;
using FateDeck.Web.Repositories.Contracts;
using Ninject;

namespace FateDeck.Web.Controllers
{
    public class ControllerBase : Controller 
    {
        [Inject]
        public IRepositoryFactory Repositories { get; set; }
    }
}