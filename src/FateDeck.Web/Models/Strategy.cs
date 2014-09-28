using FateDeck.Web.Models.Contracts;

namespace FateDeck.Web.Models
{
    public class Strategy:IEntity
    {
        public Strategy(int id, string name, string setup, string victoryPoints, string specialRules, Suite flipSuit, Source source)
        {
            Id = id;
            Name = name;
            Setup = setup;
            VictoryPoints = victoryPoints;
            FlipSuit = flipSuit;
            Source = source;
            SpecialRules = specialRules;
        }

        public Strategy(){}

        public int Id { get; set; }
        public string Name { get; set; }
        public string Setup { get; set; }
        public string SpecialRules { get; set; }
        public string VictoryPoints { get; set; }
        public Suite FlipSuit { get; set; }
        public Source Source { get; set; }
    }
}