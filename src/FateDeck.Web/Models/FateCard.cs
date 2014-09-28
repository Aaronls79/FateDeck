namespace FateDeck.Web.Models
{
    public class FateCard
    {
        public FateCard(int value, Suite suite, string key)
        {
            Value = value;
            Suite = suite;
            Key = key;
        }

        public FateCard()
        {
        }

        public string Key { get; set; }
        public Suite Suite { get; set; }
        public int Value { get; set; }
    }
}