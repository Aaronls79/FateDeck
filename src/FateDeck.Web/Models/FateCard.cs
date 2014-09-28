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

        public int Value { get; set; }
        public Suite Suite { get; set; }
        public string Key { get; set; }
    }
}