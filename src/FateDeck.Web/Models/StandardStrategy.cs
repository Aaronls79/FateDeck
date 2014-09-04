namespace FateDeck.Web.Models
{
    public class StandardStrategy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Setup { get; set; }
        public string VictoryPoints { get; set; }
        public Suite FlipSuit { get; set; }
    }
}