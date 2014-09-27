namespace FateDeck.Web.Models
{
    public class StandardEncounterViewModel
    {
        public Strategy Strategy { get; set; }
        public Deployment Deployment { get; set; }
        public Scheme[] Schemes { get; set; }
    }
}