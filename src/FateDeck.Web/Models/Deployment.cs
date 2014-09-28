using FateDeck.Web.Models.Contracts;

namespace FateDeck.Web.Models
{
    public class Deployment : IEntity
    {
        public Deployment(int id, string name, string description, int flipValueMin, int flipValueMax, Source source)
        {
            Id = id;
            Name = name;
            Description = description;
            FlipValueMax = flipValueMax;
            FlipValueMin = flipValueMin;
            Source = source;
        }

        public Deployment() { }

        public string Description { get; set; }
        public int FlipValueMax { get; set; }
        public int FlipValueMin { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public Source Source { get; set; }
    }
}