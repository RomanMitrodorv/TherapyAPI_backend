using System;
using System.Linq;
using Domain.Models;
using NHibernate;

namespace Storage.Interfaces
{
    public interface IRepository<T> where T : PersistentObject
    {
        T Load(object nodeId);

        T Get(long id);

        IQueryable<T> GetAll();

        ISQLQuery ExecuteSqlQuery(string sqlQuery);

        void Add(T entity);

        void Update(T entity);

        void Remove(T entity);
    }
}
