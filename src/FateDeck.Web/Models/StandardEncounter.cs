namespace FateDeck.Web.Models
{
    public class StandardEncounter
    {
        public StandardStrategy Strategy { get; set; }
        public Deployment Deployment { get; set; }
        public Scheme[] Schemes { get; set; }
    }
}