using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Dapper;
using FateDeck.Web.Models.Contracts;
using FateDeck.Web.Repositories.Contracts;
using FateDeck.Web.Runtime;

namespace FateDeck.Web.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class, IEntity, new()
    {
        protected RepositoryBase()
        {
            new DataSource().Create().Initialize();
        }

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
            return false;
        }

        protected virtual List<string> Fields
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
                    _fields = value;
            }
        }

        public virtual void Save(T item)
        {
            using (var cnn = DataSource.Connection())
            {
                if (item.Id <= 0)
                {
                    var sqlFields = new StringBuilder();
                    foreach (var field in Fields)
                        sqlFields.Append(string.Format("{0}, ", field));
                    var sqlParams = new StringBuilder();
                    foreach (var field in Fields)
                        sqlParams.Append(string.Format("@{0}, ", field));
                    var id = cnn.Query<int>(string.Format(
                        @"INSERT INTO {0} 
                        ( {1} ) VALUES 
                        ( {2} );
                        select last_insert_rowid()",
                        typeof(T).Name,
                        sqlFields.ToString().Trim().TrimEnd(','),
                        sqlParams.ToString().Trim().TrimEnd(',')),
                        item).First();
                    item.Id = id;
                }
                else
                {
                    var sqlFields = new StringBuilder();
                    foreach (var field in Fields)
                        sqlFields.Append(string.Format("{0} = @{0}, ", field));
                    cnn.Query<int>(string.Format(
                        @"Update {0} 
                        SET
                        ( {1} )  
                        WHERE Id = @Id",
                        typeof (T).Name,
                        sqlFields.ToString().Trim().TrimEnd(',')),
                        item);
                }
            }
        }

        public virtual T Get(int id)
        {
            using (var cnn = DataSource.Connection())
            {
                return cnn.Query<T>(string.Format(@"
                    SELECT * 
                    FROM {0}
                    WHERE Id = @Id", typeof(T).Name),
                    new { Id = id }).FirstOrDefault();
            }
        }

        public virtual void Delete(T item)
        {
            using (var cnn = DataSource.Connection())
            {
                cnn.Query<int>(string.Format(@"
                    DELETE
                    FROM {0}
                    WHERE Id = @Id", typeof(T).Name),
                    new { item.Id });
            }
        }
    }
}