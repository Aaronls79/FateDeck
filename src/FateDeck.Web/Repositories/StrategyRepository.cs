using System;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using Dapper;
using FateDeck.Web.Models;
using FateDeck.Web.Repositories.Contracts;
using FateDeck.Web.Runtime;

namespace FateDeck.Web.Repositories
{
    public class StrategyRepository : RepositoryBase<Strategy>, IStrategyRepository
    {
        public Strategy GetStandardStrategy(FateCard fateCard)
        {
            using (var cnn = DataSource.Connection())
            {
                if (fateCard.Suite != Suite.Wild && fateCard.Suite != Suite.None)
                {
                    return cnn.Query<Strategy>(@"
                            SELECT * Strategy
                            WHERE FlipSuit = @Suite
                        ", new { fateCard.Suite }
                    ).FirstOrDefault();
                }
                return cnn.Query<Strategy>(@"
                    SELECT * Strategy
                    WHERE Name = 'Stake a Claim'
                ").FirstOrDefault();
            }
        }
    }

}