using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WhiteLagoon.Application.Common.Interfaces.Infrastructure.Repository;
using WhiteLagoon.Application.Common.Interfaces.Infrastructure.Repository.Common;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;
using WhiteLagoon.Infrastructure.Repositories.Common;

namespace WhiteLagoon.Infrastructure.Repositories
{
    public class VillaRepository :GenericRepository<Villa>, IVillaRepository
    {
        private readonly ApplicationDbContext _db;
        public VillaRepository(ApplicationDbContext db):base(db) 
        {
            _db = db;
        }

        public void SaveRepo()
        {
            _db.SaveChanges();
        }

        public void UpdateRepo(Villa entity)
        {
            _db.Villas.Update(entity);  
        }
    }
}
