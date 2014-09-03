namespace FateDeck.Web.Models
{
    public class Strategy
    {
        public string Name { get; set; }
        public string Setup { get; set; }
        public string VictoryPoints { get; set; }
        public Suite[] FlipSuits { get; set; }
    }
}