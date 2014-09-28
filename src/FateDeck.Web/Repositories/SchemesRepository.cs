using System.Collections.Generic;
using System.Linq;
using Dapper;
using FateDeck.Web.Models;
using FateDeck.Web.Repositories.Contracts;
using FateDeck.Web.Runtime;

namespace FateDeck.Web.Repositories
{
    public class SchemesRepository : RepositoryBase<Scheme>, ISchemesRepository
    {
        public Scheme[] GetSchemes(params FateCard[] fateCards)
        {
            var schemes = new List<Scheme>();
            if (fateCards != null && fateCards.Any())
            {
                using (var cnn = DataSource.Connection())
                {
                    var scheme = cnn.Query<Scheme>(@"
                            SELECT * Scheme
                            WHERE Name = 'A Line in the Sand'
                        ").FirstOrDefault();
                    if (scheme != null)
                        schemes.Add(scheme);

                    foreach (FateCard fateCard in fateCards)
                    {
                        if (fateCard.Suite == Suite.Wild || fateCard.Suite == Suite.None) continue;

                        scheme = cnn.Query<Scheme>(@"
                            SELECT * Scheme
                            WHERE FlipSuit = @Suite
                        ", new { fateCard.Suite }
                        ).FirstOrDefault();
                        if (scheme != null && !schemes.Exists(x => x.Id == scheme.Id))
                            schemes.Add(scheme);

                        scheme = cnn.Query<Scheme>(@"
                            SELECT * Scheme
                            WHERE FlipValue = @Value
                        ", new { fateCard.Value }
                        ).FirstOrDefault();
                        if (scheme != null && !schemes.Exists(x => x.Id == scheme.Id))
                            schemes.Add(scheme);
                    }
                    if ((schemes.Count - 1) != (fateCards.Count(x => x.Suite == Suite.Wild || x.Suite == Suite.None) * 2))
                    {
                        scheme = cnn.Query<Scheme>(@"
                            SELECT * Scheme
                            WHERE Name = 'Distract'
                        ").FirstOrDefault();
                        if (scheme != null && !schemes.Exists(x => x.Id == scheme.Id))
                            schemes.Add(scheme);
                    }
                }
            }
            return schemes.ToArray();
        }
    }
}