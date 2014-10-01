using FateDeck.Web.Models.Contracts;

namespace FateDeck.Web.Models
{
    public class Scheme : IEntity
    {
        public Scheme(int id, string name, string description, int flipValue, Suite flipSuit, Source source)
        {
            Id = id;
            Name = name;
            Description = description;
            FlipValue = flipValue;
            FlipSuit = flipSuit;
            Source = source;
        }

        public Scheme(){}

        public string Description { get; set; }
        public Suite FlipSuit { get; set; }
        public int FlipValue { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public Source Source { get; set; }
    }
}