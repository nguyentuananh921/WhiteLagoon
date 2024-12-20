﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WhiteLagoon.Application.Common.Interfaces.Infrastructure.Repository.Common;
using WhiteLagoon.Application.Common.Interfaces.Infrastructure.Repository.WhiteLagoon;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Domain.Entities.WhiteLagoon;
using WhiteLagoon.Infrastructure.Data;
using WhiteLagoon.Infrastructure.Repositories.Common;

namespace WhiteLagoon.Infrastructure.Repositories.WhiteLagoon
{
    public class AmenityRepository : GenericRepository<Amenity>, IAmenityRepository
    {
        private readonly ApplicationDbContext _db;
        public AmenityRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
