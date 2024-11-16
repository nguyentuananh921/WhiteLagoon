using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteLagoon.Application.Common.Interfaces.Infrastructure.Repository.Bulky;
using WhiteLagoon.Application.Common.Interfaces.Infrastructure.Repository.WhiteLagoon;

namespace WhiteLagoon.Application.Common.Interfaces.Infrastructure.Repository.Common
{
    public interface IUnitOfWork
    {
        IVillaRepository VillaRepo { get; }
        IVillaNumberRepository VillaNumberRepo { get; }
        IAmenityRepository AmenityRepo { get; }
        ICategoryRepository CategoryRepo { get; }
        IProductRepository ProductRepo { get; }
        IProductImageRepository ProductImageRepo { get; }
        ICompanyRepository CompanyRepo { get; }
        void SaveUnitOfWork();
    }
}
