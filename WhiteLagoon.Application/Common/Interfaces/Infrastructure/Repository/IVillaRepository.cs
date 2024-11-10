using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WhiteLagoon.Domain.Entities;

namespace WhiteLagoon.Application.Common.Interfaces.Infrastructure.Repository
{
    public interface IVillaRepository
    {
        IEnumerable<Villa> GetAllRepo(Expression<Func<Villa,bool>> ?filter=null,string? includeProperties=null);
        Villa GetByIdRepo(Expression<Func<Villa, bool>> filter, string? includeProperties = null);
        void AddRepo(Villa entity);
        void RemoveRepo(Villa entity);
        void UpdateRepo(Villa entity);
        void Save();
    }
}
