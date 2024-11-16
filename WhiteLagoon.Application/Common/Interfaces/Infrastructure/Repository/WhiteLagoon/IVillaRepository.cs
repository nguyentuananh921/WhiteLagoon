using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WhiteLagoon.Application.Common.Interfaces.Infrastructure.Repository.Common;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Domain.Entities.WhiteLagoon;

namespace WhiteLagoon.Application.Common.Interfaces.Infrastructure.Repository.WhiteLagoon
{
    public interface IVillaRepository : IGenericRepository<Villa>
    {
    }
}
