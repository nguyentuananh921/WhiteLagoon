﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteLagoon.Application.Common.Interfaces.Infrastructure.Repository.Bulky;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Domain.Entities.Bulky;
using WhiteLagoon.Infrastructure.Data;
using WhiteLagoon.Infrastructure.Repositories.Common;

namespace WhiteLagoon.Infrastructure.Repositories.Bulky
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
