using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WhiteLagoon.Application.Common.Interfaces.Infrastructure.Repository;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLagoon.Infrastructure.Repositories
{
    public class VillaRepository : IVillaRepository
    {
        private readonly ApplicationDbContext _db;
        public VillaRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public void AddRepo(Villa entity)
        {
            _db.Add(entity);
        }

        public IEnumerable<Villa> GetAllRepo(Expression<Func<Villa, bool>>? filter = null, string? includeProperties = null)
        {
            //IQueryable<Villa> query = _db.Villas();
            IQueryable<Villa> query = _db.Set<Villa>();
            if (filter != null) 
            {
                query=query.Where(filter);
            }
            if (!string.IsNullOrEmpty(includeProperties))
            {
                //Villa, VillaNumber --Case Sensitive
                foreach (var includePro in includeProperties
                    .Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries)) 
                { 
                    query=query.Include(includePro);
                }
            }
            return query.ToList();
        }

        public Villa GetByIdRepo(Expression<Func<Villa, bool>> filter, string? includeProperties = null)
        {
            IQueryable<Villa> query = _db.Set<Villa>();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (!string.IsNullOrEmpty(includeProperties))
            {
                //Villa, VillaNumber --Case Sensitive
                foreach (var includePro in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includePro);
                }
            }
            return query.FirstOrDefault();
        }

        public void RemoveRepo(Villa entity)
        {
            _db.Remove(entity);
        }

        public void SaveRepo()
        {
            _db.SaveChanges();
        }

        public void UpdateRepo(Villa entity)
        {
            _db.Update(entity);
            //_db.Villas.Update(entity);  
        }
    }
}
