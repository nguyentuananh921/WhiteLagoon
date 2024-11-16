using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteLagoon.Application.Common.Interfaces.Infrastructure.Repository.Bulky;
using WhiteLagoon.Application.Common.Interfaces.Infrastructure.Repository.Common;
using WhiteLagoon.Application.Common.Interfaces.Infrastructure.Repository.WhiteLagoon;
using WhiteLagoon.Infrastructure.Data;
using WhiteLagoon.Infrastructure.Repositories.Bulky;
using WhiteLagoon.Infrastructure.Repositories.WhiteLagoon;

namespace WhiteLagoon.Infrastructure.Repositories.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IVillaRepository VillaRepo { get; private set; }
        public IVillaNumberRepository VillaNumberRepo { get; private set; }

        public IAmenityRepository AmenityRepo { get; private set; }

        public ICategoryRepository CategoryRepo { get; private set; }

        public IProductRepository ProductRepo { get; private set; }

        public IProductImageRepository ProductImageRepo { get; private set; }

        public ICompanyRepository CompanyRepo { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            #region WhiteLagoon
            VillaRepo = new VillaRepository(_db);
            VillaNumberRepo = new VillaNumberRepository(_db);
            AmenityRepo = new AmenityRepository(_db);
            #endregion
            #region Bulky
            CategoryRepo = new CategoryRepository(_db);
            ProductRepo = new ProductRepository(_db);
            ProductImageRepo = new ProductImageRepository(_db);
            CompanyRepo = new CompanyRepository(_db);
            #endregion

        }

        public void SaveUnitOfWork()
        {
            _db.SaveChanges();
        }
    }
}
