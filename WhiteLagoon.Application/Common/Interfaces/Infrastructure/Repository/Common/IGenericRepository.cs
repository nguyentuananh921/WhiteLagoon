using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WhiteLagoon.Domain.Entities;

namespace WhiteLagoon.Application.Common.Interfaces.Infrastructure.Repository.Common
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAllRepo(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        T GetRepo(Expression<Func<T, bool>> filter, string? includeProperties = null);
        void AddRepo(T entity);
        void RemoveRepo(T entity);       

    }
}
