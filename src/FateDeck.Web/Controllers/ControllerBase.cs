using System.Web.Mvc;
using FateDeck.Web.Repositories;
using FateDeck.Web.Repositories.Contracts;

namespace FateDeck.Web.Controllers
{
    public class ControllerBase : Controller 
    {
        private IRepositoryFactory _repositories;
        public IRepositoryFactory Repositories
        {
            get
            {
                if (_repositories == null)
                    _repositories = new RepositoryFactory();
                return _repositories;
            }
            set { _repositories = value; }
        }
    }
}