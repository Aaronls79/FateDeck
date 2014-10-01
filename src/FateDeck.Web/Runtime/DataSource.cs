using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using Dapper;
using FateDeck.Web.Models;

namespace FateDeck.Web.Runtime
{
    public class DataSource
    {
        public static string DbFile
        {
            get { return Environment.CurrentDirectory + "\\FateDeck.sqlite"; }
        }

        public static SQLiteConnection Connection()
        {
            return new SQLiteConnection("Data Source=" + DbFile);
        }

        public DataSource Delete()
        {
            if (!File.Exists(DbFile)) return this;
            File.Delete(DbFile);
            return this;
        }

        public DataSource Create()
        {
            if (File.Exists(DbFile)) return this;

            using (var cnn = Connection())
            {
                cnn.Open();
                cnn.Execute(
                  @"create table Scheme
                  (
                     Id             INTEGER PRIMARY KEY,
                     Name           varchar(128) not null,
                     Description    varchar(512) not null,
                     FlipSuit      integer not null,
                     Source         integer not null,
                     FlipValue      integer not null
                  )"
              );
                cnn.Execute(
                   @"create table Strategy
                  (
                     Id             INTEGER PRIMARY KEY,
                     Name           varchar(128) not null,
                     Setup          varchar(512) not null,
                     VictoryPoints  varchar(512) not null,
                     SpecialRules   varchar(512) not null,
                     FlipSuit      integer not null,
                     Source         integer not null
                  )"
                 );
                cnn.Execute(
                  @"create table Deployment
                  (
                     Id                INTEGER PRIMARY KEY,
                     Name              varchar(128) not null,
                     Description       varchar(512) not null,
                     FlipValueMax      integer not null,
                     FlipValueMin      integer not null,
                     Source            integer not null
                  )"
                );
            }
            return this;
        }

        public void Initialize()
        {
            InitializeStrategies();
            InitializeDeployments();
            InitializeSchemes();
        }

        private static void InitializeDeployments()
        {
            using (var cnn = Connection())
            {
                var deployments = new List<Deployment>
                {
                    new Deployment(0,"Standard Deployment","A player will deploy within 6\" of a chosen Table Edge,with the opponent deploying within 6\" of the opposite Table Edge.", 1, 7, Source.CoreRulebook2ndEdition),
                    new Deployment(0,"Corner Deployment","A player will deploy within 12\" of a chosen Table Corner, with the opponent deploying within 12\" of the opposite Table Corner.", 8, 10, Source.CoreRulebook2ndEdition),
                    new Deployment(0,"Flank Deployment","The table is divided into four 18\" x 18\" Quarters. A player will deploy within 9\" of the table edges within one table Quarter, with the opponent deploying within 9\" of the table edges within the opposite table Quarter.", 11, 13, Source.CoreRulebook2ndEdition),
                    new Deployment(0,"Close Deployment","A player will deploy within 12\" of a chosen Table Edge, with the opponent deploying within 12\" of the opposite Table Edge.", 14, 14, Source.CoreRulebook2ndEdition),
                    new Deployment(0,"Close Deployment","A player will deploy within 12\" of a chosen Table Edge, with the opponent deploying within 12\" of the opposite Table Edge.", 0, 0, Source.CoreRulebook2ndEdition),
                };

                foreach (Deployment deployment in deployments)
                {
                    cnn.Execute(
                        @"INSERT INTO Deployment 
                        ( Id, Name, Description, FlipValueMax, FlipValueMin, Source ) VALUES 
                        ( NULL, @Name, @Description, @FlipValueMax, @FlipValueMin, @Source )"
                        , deployment);
                }
            }
        }

        private static void InitializeSchemes()
        {
            using (var cnn = Connection())
            {
                var schemes = new List<Scheme>
                {
                    new Scheme(0,"A Line in the Sand","At the end of the game, the Crew earns 2 VP if it has at least four Scheme Markers on the Centerline.<br/>If this Scheme is revealed, the Crew earns an additional VP if it has at least two Scheme markers on the Centerline at the end of the game.", 0, Suite.None, Source.CoreRulebook2ndEdition),
                    new Scheme(0,"Distract","All non-Peon models in this Crew may target a non-Peon enemy model within 1\" with a (1) Interact Action to give the target the following Condition for the rest of the game: \"Distracted: This model may take a (2) Interact Action to remove this Condition from itself. No other Action may remove this Condition.\" This Scheme starts the game unrevealed. The first time an enemy model gains the Distracted Condition, reveal this Scheme. At the end of every Turn, this Crew earns 1 VP if at least two enemy models have the Distracted Condition.", 0, Suite.None, Source.CoreRulebook2ndEdition),
                    new Scheme(0,"Breakthrough","At the end of the game, this Crew earns 1 VP for each of its Scheme Markers within 6\" of the enemy Deployment Zone. If this Scheme is revealed and this Crew earns at least 2 VP from this Scheme, it earns 1 additional VP.", 0, Suite.Masks, Source.CoreRulebook2ndEdition),
                    new Scheme(0,"Assassinate","This Scheme begins the game unrevealed. If the enemy Leader is killed or sacrificed, reveal this Scheme. If the enemy Leader is killed or sacrificed, gain 2 VP. If this happens on or before Turn 4, score 3 VP instead.", 0, Suite.Crows, Source.CoreRulebook2ndEdition),
                    new Scheme(0,"Protect Territory","At the end of the game, this Crew gains 1 VP for each of its Scheme Markers which is at least 6\" from its Deployment Zone and has at least one friendly non-Peon model within 2\" of it. Scheme Markers with more enemy models than friendly models within 2\" do not count towards this Scheme.<br/>If this Scheme is revealed and this Crew earns at least 2 VP from this Scheme, it earns 1 additional VP.", 0, Suite.Tombs, Source.CoreRulebook2ndEdition),
                    new Scheme(0,"Bodyguard","The scheming player notes down a non-Leader Henchman or Enforcer model in her Crew that must be protected. If the Crew has no Henchmen or Enforcer models, note down the model with the highest Soulstone cost instead.<br/>This Scheme may be revealed at any time. At the end of every Turn, starting on Turn 4, if this Scheme is revealed, this Crew earns 1 VP if the noted model is still in play and at least 8\" from its Deployment Zone. At the end of the game, this Crew earns 1 additional VP if the noted model is still in play with more than half of its Wounds remaining.", 0, Suite.Rams, Source.CoreRulebook2ndEdition),
                    new Scheme(0,"Cursed Object","All non-Peon models in this Crew may target a non-Peon enemy model within 1\" with a (1) Interact Action to give the target the following Condition for the rest of the game:<br/>\"Cursed Object: This model may take a (1) Interact Action to perform a TN 12 Wk duel. If successful, remove this Condition from this model. No other Action may remove this Condition.\"<br/> This Scheme starts the game unrevealed. The first time an enemy model gains the Cursed Object Condition, reveal this Scheme. At the end of every Turn after the first, this Crew may end the Cursed Object Condition on one enemy model to gain 1 VP.", 1, Suite.None, Source.CoreRulebook2ndEdition),
                    new Scheme(0,"Outflank","At the end of the game, this Crew earns 1 VP if it has a non-Peon model on the Centerline and within 3\" of the point where the Centerline meets the table edge (or corner). This Crew earns an additional 1 VP if it has another non-Peon model on the Centerline within 3\" of the opposite point where the Centerline meets the table edge (or corner). Models which are engaged with an enemy may not count towards this Scheme.<br/> If this Scheme is revealed, this Crew earns an additional 1 VP if it has at least one non-Peon model within 3\" of the point where the Centerline meets the table edge (or corner).", 2, Suite.None, Source.CoreRulebook2ndEdition),
                    new Scheme(0,"Plant Evidence","At the end of the game, the Crew earns 1 VP for each piece of terrain in base contact with at least one of the Crew's Scheme Markers, if the Scheme Marker is within the Enemy Half of the table.<br/>If this Scheme is revealed and this Crew earns at least 2 VP from this Scheme, it earns 1 additional VP.", 3, Suite.None, Source.CoreRulebook2ndEdition),
                    new Scheme(0,"Entourage","The scheming player chooses a Master or Henchman model in her crew. At the end of the game, if the chosen model is in the Enemy Half of the table, the Crew earns 1 VP.<br/>If the chosen model is in the enemy's Deployment Zone at the end of the game the Crew earns 2 VP instead.<br/>If this Scheme is revealed, this crew earns 1 additional VP if it earns any VP from this Scheme.", 4, Suite.None, Source.CoreRulebook2ndEdition),
                    new Scheme(0,"Vendetta","The scheming player notes one of her non-Leader, non-Peon models with a Soulstone cost greater than 0 and an enemy model with a Soulstone cost equal to or greater than her chosen model. If the noted friendly model's first Attack Action in the game is against the noted enemy model, score 1 VP and reveal this Scheme. If the noted enemy model is not in play at the end of the game, and this Scheme has been revealed, score 1 additional VP. If the noted enemy model is killed by the noted friendly model, score 3 VP (whether or not the Scheme was revealed).<br/>This Scheme may not be revealed at the start of the game.", 5, Suite.None, Source.CoreRulebook2ndEdition),
                    new Scheme(0,"Plant Explosives","Once per game, at the end of any Turn, this Crew may reveal this Scheme and earn 1 VP for each enemy model that is within 3\" of at least one of this Crew's Scheme Markers. Then, remove all of this Crew's Scheme Markers which are within 3\" of an enemy model.<br/>This Scheme does not benefit from being revealed.", 6, Suite.None, Source.CoreRulebook2ndEdition),
                    new Scheme(0,"Make Them Suffer","At the end of every Turn after the first in which at least one enemy Minion or Peon model was killed by one of this Crew's Henchman or Master models, score 1 VP. At the end of every Turn after the first, if the opposing Crew has no Minion or Peon models, score 1 VP. No more than 1 VP per Turn may be scored from this Scheme. This Scheme must be revealed as soon as any VP are scored from it.", 7, Suite.None, Source.CoreRulebook2ndEdition),
                    new Scheme(0,"Deliver a Message","This Crew's non-Leader, non-Peon models may take a (2) Interact Action targeting an enemy Leader they are engaged with to reveal this Scheme and earn 2 VP. This Action can only be taken once during the game.<br/>If this Scheme is revealed at the start of the game, this crew earns 3 VP instead of 2 VP if it achieves this Scheme.", 8, Suite.None, Source.CoreRulebook2ndEdition),
                    new Scheme(0,"Take Prisoner","The scheming player notes down a model in the opponent's crew. At the end of the game, if this Crew has at least one non-Peon model engaged with the noted enemy model this Crew earns 2 VP. If there are no other enemy models within 3\" of the chosen model, and this Crew has at least one non-Peon model engaged with the chosen model, this Crew earns 3 VP instead. This Scheme does not benefit from being revealed.", 9, Suite.None, Source.CoreRulebook2ndEdition),
                    new Scheme(0,"Spring the Trap","Once per game, at the end of any Turn the scheming player may reveal this Scheme. This Crew earns 1 VP for every Scheme Marker it has within 4\" of the enemy Leader, then remove all of this Crew's Scheme Markers within 4\" of the enemy Leader. If the enemy Crew has as many or more models in play than this Crew when this Scheme is revealed, and at least 1 VP is scored from this Scheme, score an additional VP. This Scheme may not be revealed at the start of the game.", 10, Suite.None, Source.CoreRulebook2ndEdition),
                    new Scheme(0,"Murder Protege","Note down the enemy model with the highest Soulstone Cost. If multiple models are tied for the highest Soulstone Cost, then any of those models may be noted down. This Crew earns 2 VP if the noted enemy model is killed or sacrificed before the end of the game.<br/>If this Scheme is revealed, this crew earns 3 VP instead of 2 VP if it achieves this Scheme.", 11, Suite.None, Source.CoreRulebook2ndEdition),
                    new Scheme(0,"Frame for Murder","The scheming player notes one of her own non-Peon models as the \"sucker.\" If the chosen \"sucker\" model is killed or sacrificed by an enemy model, score 1 VP. If the enemy model was a Master or Henchman, score 2 VP instead. As soon as this Scheme is accomplished, reveal it. If it was accomplished before Turn 4, score 1 additional VP.<br/>This Scheme may not be revealed at the start of the game.", 12, Suite.None, Source.CoreRulebook2ndEdition),
                    new Scheme(0,"Power Ritual","At the end of the game, for each Table Corner that this Crew has a Scheme marker within 6\" of, this Crew earns 1 VP. Only one Table Corner within this Crew's Deployment Zone may count towards this Scheme. If this Scheme is revealed and this Crew earns at least 2 VP from this Scheme, it earns 1 additional VP.", 13, Suite.None, Source.CoreRulebook2ndEdition),
                };

                foreach (Scheme scheme in schemes)
                {
                    cnn.Execute(
                        @"INSERT INTO Scheme 
                        ( Id, Name, Description, FlipValue, FlipSuit, Source ) VALUES 
                        ( NULL, @Name, @Description, @FlipValue, @FlipSuit, @Source );"
                        , scheme);
                }
            }
        }

        private static void InitializeStrategies()
        {
            using (var cnn = Connection())
            {
                var strategies = new List<Strategy>
                {
                    new Strategy(0,"Turf War","Place a single Turf Marker at the Center of the table.","At the end of each Turn after the first, a Crew earns 1 VP if it has two or more non-Peon models within 6\" of the Turf Marker.","None",Suite.Rams,Source.CoreRulebook2ndEdition),
                    new Strategy(0,"Reckoning","None","At the end of every Turn, after the first, a Crew earns 1 VP if it killed or sacrificed two or more enemy models during that Turn.<br/>At the end of every Turn after the first, if a player has no models in play (buried models are not considered \"in play\") then her opponent earns 1 VP. A player may not earn more than 1 VP from this Strategy per Turn.","None",Suite.Crows,Source.CoreRulebook2ndEdition),
                    new Strategy(0,"Reconnoiter","Divide the table into four 18\" x 18\" table Quarters.","At the end of each Turn after the first, a Crew earns 1VP if it controls two or more table Quarters. To control a table quarter, the Crew must have the most non-Peon models within the table Quarter. These models cannot be within 6\" of the Center of the table, or partially within another table Quarter.","None",Suite.Masks,Source.CoreRulebook2ndEdition),
                    new Strategy(0,"Squatter's Rights","Place five 30mm Squat Markers along the Centerline. One is placed at the Center of the table. Then, two more are placed on the Centerline 6\" away from the Center of the table (one on each side). Lastly, two more are placed on the Centerline 6\" away from table's edge (one on each side).","At the end of each Turn after the first, a Crew earns 1 VP if it has claim to at least two Squat Markers.","Squat Markers begin the game claimed by neither Crew.<br/>A model may take a (1) Interact Action to claim any Squat marker that is in base contact with the model. A Squat marker is only ever claimed by the last Crew to interact with it, all previous claims are removed.",Suite.Tombs,Source.CoreRulebook2ndEdition),
                    new Strategy(0,"Stake a Claim","None","At the end of each Turn after the first, a Crew earns 1 VP if there are more Claim Markers on the Enemy Half of the table than its own.","A model may take a (2) Interact Action to discard all Claim Markers within 6\" of itself, and then place a Claim Marker in base contact with itself.",Suite.None | Suite.Wild,Source.CoreRulebook2ndEdition)
                };
                foreach (Strategy strategy in strategies)
                {
                    cnn.Execute(
                        @"INSERT INTO Strategy 
                        ( Id, Name, Setup, VictoryPoints, FlipSuit, Source, SpecialRules ) VALUES 
                        ( NULL, @Name, @Setup, @VictoryPoints, @FlipSuit, @Source, @SpecialRules )"
                        , strategy);
                }
            }
        }
    }
}