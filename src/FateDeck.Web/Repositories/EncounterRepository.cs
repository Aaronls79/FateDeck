using System;
using FateDeck.Web.Models;

namespace FateDeck.Web.Repositories
{
    public class EncounterRepository
    {
        public Scheme[] GetSchemes(params Models.FateCard[] fateCards)
        {
            throw new NotImplementedException();
        }

        public Deployment GetDeployment(Models.FateCard fateCard)
        {
            throw new NotImplementedException();
        }

        public StandardStrategy GetStandardStrategy(Models.FateCard fateCard)
        {
            throw new NotImplementedException();            
        }
    }

}