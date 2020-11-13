using System;
using System.Collections.Generic;
using System.Text;
using static DealerPlatformApiDemo.Core.Data.RepositoryCompare;

namespace DealerPlatformApiDemo.Core.Data
{
    //IRepository`1
    public interface IRepository<TEntity> where TEntity:BaseEntity
    {
        public IEnumerable<TEntity> ListAll();
        public IEnumerable<TEntity> ListById(int id);

        public IEnumerable<TEntity> ListByCustom(string fieldName, string fieldValue, CompareType compareType = CompareType.Equal);
        public IEnumerable<TEntity> ListByCustoms(Dictionary<string, string> keyValuePairs);
        public IEnumerable<TEntity> ListByCustoms(Dictionary<string, ListByCustomsEntity> keyValuePairs);

        public IEnumerable<TEntity> ListByCustomWhereIn(string fieldName, params string[] fieldValues);

        public int Insert(TEntity entity);
        public object InsertBackRecord(TEntity entity);

        public int Update(TEntity entity);

        public int Delete(TEntity entity);
        public int Delete(int id);

        public int DeleteByCustomer(string fieldName, string fieldValue);
    }
}
