using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using Dapper;
using FateDeck.Web.Models;

namespace FateDeck.Web.Repositories
{
    public class FateDeckRepositoryBase<T>
    {

        private List<string> _fields;

        private static bool SimpleType(Type propertyType)
        {
            return
                (propertyType == typeof(int)) |
                (propertyType == typeof(string)) |
                (propertyType == typeof(bool)) |
                (propertyType == typeof(long)) |
                (propertyType == typeof(Enum)) |
                (propertyType == typeof(byte));
        }

        protected virtual bool ExcludedProperty(PropertyInfo propertyInfo)
        {
            return true;
        }

        public virtual List<string> Fields
        {
            get
            {
                if (_fields == null)
                {
                    _fields = new List<string>();
                    var fieldNames = new List<string>();
                    fieldNames.AddRange(typeof(T).GetProperties()
                        .Where(x => SimpleType(x.PropertyType) && !ExcludedProperty(x))
                        .OrderBy(x => x.Name)
                        .Select(propertyInfo => propertyInfo.Name));
                }
                return _fields;
            }
            set
            {
                if (value != null)
                {
                    _fields = value;
                }
            }
        }

        public void Save(T item)
        {

        }

        public T Get(int id)
        {

        }

        public static string DbFile
        {
            get { return Environment.CurrentDirectory + "\\App_Data\\FateDeck.sqlite"; }
        }

        public static SQLiteConnection DbConnection()
        {
            return new SQLiteConnection("Data Source=" + DbFile);
        }

        public static void InitializeDataSource()
        {
            if (File.Exists(DbFile)) return;

            using (var cnn = DbConnection())
            {
                cnn.Open();
                cnn.Execute(
                  @"create table Scheme
                  (
                     Id             integer identity primary key AUTOINCREMENT,
                     Name           varchar(128) not null,
                     Description    varchar(512) not null,
                     FlipSuite      integer not null,
                     Source         integer not null,
                     FlipValue      integer not null
                  )"
              );
                cnn.Execute(
                   @"create table Strategy
                  (
                     Id             integer identity primary key AUTOINCREMENT,
                     Name           varchar(128) not null,
                     Setup          varchar(512) not null,
                     VictoryPoints  varchar(512) not null,
                     SpecialRules   varchar(512) not null,
                     FlipSuite      integer not null,
                     Source         integer not null
                  )"
                 );
                cnn.Execute(
                  @"create table Deployment
                  (
                     Id             integer identity primary key AUTOINCREMENT,
                     Name           varchar(128) not null,
                     Description    varchar(512) not null,
                     VictoryPoints  varchar(512) not null,
                     FlipValue      integer not null,
                     Source         integer not null
                  )"
                );
                var strategies = new List<Strategy>
                {
                    new Strategy("Turf War",
                        "Place a single Turf Marker at the Center of the table.",
                        "At the end of each Turn after the first, a Crew earns 1 VP if it has two or more non-Peon models within 6\" of the Turf Marker.",
                        "None",
                        Suite.Rams,
                        Source.CoreRulebook2ndEdition
                    ),
                    new Strategy("Reckoning",
                        "None",
                        "At the end of every Turn, after the first, a Crew earns 1 VP if it killed or sacrificed two or more enemy models during that Turn.<br/>At the end of every Turn after the first, if a player has no models in play (buried models are not considered \"in play\") then her opponent earns 1 VP. A player may not earn more than 1 VP from this Strategy per Turn.",
                        "None",
                        Suite.Crows,
                        Source.CoreRulebook2ndEdition
                    ),
                    new Strategy("Reconnoiter",
                        "Divide the table into four 18\" x 18\" table Quarters.",
                        "At the end of each Turn after the first, a Crew earns 1VP if it controls two or more table Quarters. To control a table quarter, the Crew must have the most non-Peon models within the table Quarter. These models cannot be within 6\" of the Center of the table, or partially within another table Quarter.",
                        "None",
                        Suite.Masks,
                        Source.CoreRulebook2ndEdition
                    ),
                    new Strategy("Squatter's Rights",
                        "Place five 30mm Squat Markers along the Centerline. One is placed at the Center of the table. Then, two more are placed on the Centerline 6\" away from the Center of the table (one on each side). Lastly, two more are placed on the Centerline 6\" away from table's edge (one on each side).",
                        "At the end of each Turn after the first, a Crew earns 1 VP if it has claim to at least two Squat Markers.",
                        "Squat Markers begin the game claimed by neither Crew.<br/>A model may take a (1) Interact Action to claim any Squat marker that is in base contact with the model. A Squat marker is only ever claimed by the last Crew to interact with it, all previous claims are removed.",
                        Suite.Tombs,
                        Source.CoreRulebook2ndEdition
                    ),
                    new Strategy("Stake a Claim",
                        "None",
                        "At the end of each Turn after the first, a Crew earns 1 VP if there are more Claim Markers on the Enemy Half of the table than its own.",
                        "A model may take a (2) Interact Action to discard all Claim Markers within 6\" of itself, and then place a Claim Marker in base contact with itself.",
                        Suite.None | Suite.Wild,
                        Source.CoreRulebook2ndEdition
                    )
                };
                foreach (var strategy in strategies)
                {
                    cnn.Query<long>(
                        @"INSERT INTO Strategy 
                        ( Name, Setup, VictoryPoints, FlipSuit, Source ) VALUES 
                        ( @Name, @Setup, @VictoryPoints, @FlipSuit, @Source );
                        select last_insert_rowid()", strategy).First();
                }
            }
        }
    }

    public class EncounterRepository
    {
        public Scheme[] GetSchemes(params FateCard[] fateCards)
        {
            throw new NotImplementedException();
        }

        public Deployment GetDeployment(FateCard fateCard)
        {
            throw new NotImplementedException();
        }

        public Strategy GetStandardStrategy(FateCard fateCard)
        {
            throw new NotImplementedException();
        }
    }

}