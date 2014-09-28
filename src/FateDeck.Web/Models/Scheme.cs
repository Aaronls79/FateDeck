using FateDeck.Web.Models.Contracts;

namespace FateDeck.Web.Models
{
    public class Scheme : IEntity
    {
        public Scheme(int id, string name, string description, int flipValue, Suite flipSuite, Source source)
        {
            Id = id;
            Name = name;
            Description = description;
            FlipValue = flipValue;
            FlipSuite = flipSuite;
            Source = source;
        }

        public Scheme(){}

        public string Description { get; set; }
        public Suite FlipSuite { get; set; }
        public int FlipValue { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public Source Source { get; set; }
    }
}