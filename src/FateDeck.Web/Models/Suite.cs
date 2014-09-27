using System;

namespace FateDeck.Web.Models
{
    [Flags]
    public enum Suite
    {
        None = 0,
        Rams = 1,
        Crows = 2,
        Tombs = 4,
        Masks = 8,
        Wild = 16
    }
}