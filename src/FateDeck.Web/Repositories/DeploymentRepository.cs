﻿using System;
using System.Linq;
using Dapper;
using FateDeck.Web.Models;
using FateDeck.Web.Runtime;

namespace FateDeck.Web.Repositories
{
    public class DeploymentRepository : RepositoryBase<Deployment>
    {
        public Deployment GetDeployment(FateCard fateCard)
        {
            using (var cnn = DataSource.Connection())
            {
                if (fateCard.Suite != Suite.Wild && fateCard.Suite != Suite.None)
                {
                    return cnn.Query<Deployment>(@"
                            SELECT * Deployment
                            WHERE FlipValueMax <= @Value AND FlipValueMin >= @Value
                        ", new {fateCard.Value}
                    ).FirstOrDefault();
                }
                return cnn.Query<Deployment>(@"
                            SELECT * Deployment
                            WHERE Name = 'Close Deployment'
                        ").FirstOrDefault();
            }
        }
    }
}