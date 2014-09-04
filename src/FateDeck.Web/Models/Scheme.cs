namespace FateDeck.Web.Models
{
    public class Scheme
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int FlipValue { get; set; }
        public Suite FlipSuite { get; set; }
    }
}