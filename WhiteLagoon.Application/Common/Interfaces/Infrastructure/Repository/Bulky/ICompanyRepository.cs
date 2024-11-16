using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteLagoon.Application.Common.Interfaces.Infrastructure.Repository.Common;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Domain.Entities.Bulky;

namespace WhiteLagoon.Application.Common.Interfaces.Infrastructure.Repository.Bulky
{
    public interface ICompanyRepository : IGenericRepository<Company>
    {
    }
}
