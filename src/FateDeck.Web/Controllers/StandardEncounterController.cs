using System.Collections.Generic;
using System.Web.Http;
using FateDeck.Web.Models;

namespace FateDeck.Web.Controllers
{
    public class StandardEncounterController : ApiControllerBase
    {
        // GET api/standardencounter
        public IHttpActionResult Get()
        {
            var standardEncounter = new StandardEncounterViewModel();
            var fateDeck = new FateCardDeck();
            fateDeck.Shuffle();
            standardEncounter.Deployment = Repositories.DeploymentRepository.GetDeployment(fateDeck.Flip());
            standardEncounter.Strategy = Repositories.StrategyRepository.GetStandardStrategy(fateDeck.Flip());
            fateDeck.Shuffle();
            var cards = new List<FateCard>();
            while (cards.Count < 2)
            {
                var card = fateDeck.Flip();
                if (card.Suite != Suite.None && card.Suite != Suite.Wild)
                    cards.Add(card);
            }
            standardEncounter.Schemes = Repositories.SchemesRepository.GetSchemes(cards.ToArray());
            return Ok(standardEncounter);
        }
    }
}
