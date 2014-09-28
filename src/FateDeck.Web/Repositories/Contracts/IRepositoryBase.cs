using FateDeck.Web.Models.Contracts;

namespace FateDeck.Web.Repositories.Contracts
{
    public interface IRepositoryBase<T> where T : class, IEntity, new()
    {
        void Save(T item);
        T Get(int id);
        void Delete(T item);
    }
}