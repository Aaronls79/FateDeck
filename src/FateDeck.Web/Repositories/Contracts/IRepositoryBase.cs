using FateDeck.Web.Models.Contracts;

namespace FateDeck.Web.Repositories.Contracts
{
    public interface IRepositoryBase<T> where T : class, IEntity, new()
    {
        void Delete(T item);
        T Get(int id);
        void Save(T item);
    }
}