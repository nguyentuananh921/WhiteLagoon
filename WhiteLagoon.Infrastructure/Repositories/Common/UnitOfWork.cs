using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteLagoon.Application.Common.Interfaces.Infrastructure.Repository;
using WhiteLagoon.Application.Common.Interfaces.Infrastructure.Repository.Common;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLagoon.Infrastructure.Repositories.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IVillaRepository VillaRepo { get; private set; }
        public IVillaNumberRepository VillaNumberRepo { get; private set; }

        public IAmenityRepository AmenityRepo { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            VillaRepo=new VillaRepository(_db);
            VillaNumberRepo=new VillaNumberRepository(_db);
            AmenityRepo = new AmenityRepository(_db);
        }

        public void SaveUnitOfWork()
        {
            _db.SaveChanges();
        }
    }
}
