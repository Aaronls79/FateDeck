using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            var cards = new List<FateCard>();
            while (cards.Count <= 2)
                cards.Add(fateDeck.Flip());
            standardEncounter.Schemes = Repositories.SchemesRepository.GetSchemes(cards.ToArray());
            return Ok(standardEncounter);
        }
    }
}
