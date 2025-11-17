using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        T? GetById(int id);
        T? GetById(Guid id);
        T Save(T entity);
        void Delete(T entity);
    }
}
