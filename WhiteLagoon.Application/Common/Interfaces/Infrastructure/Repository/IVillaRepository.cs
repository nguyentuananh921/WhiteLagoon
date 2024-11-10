﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WhiteLagoon.Application.Common.Interfaces.Infrastructure.Repository.Common;
using WhiteLagoon.Domain.Entities;

namespace WhiteLagoon.Application.Common.Interfaces.Infrastructure.Repository
{
    public interface IVillaRepository:IGenericRepository<Villa>
    {    
        void UpdateRepo(Villa entity);
        void SaveRepo();
    }
}
