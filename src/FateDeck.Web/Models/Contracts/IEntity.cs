namespace FateDeck.Web.Models.Contracts
{
    public interface IEntity
    {
        int Id { get; set; }

        Source Source { get; set; }
    }
}