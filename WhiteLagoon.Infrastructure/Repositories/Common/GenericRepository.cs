﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WhiteLagoon.Application.Common.Interfaces.Infrastructure.Repository.Common;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLagoon.Infrastructure.Repositories.Common
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> _dbSet;
        public GenericRepository(ApplicationDbContext dbContext)
        {
                _db= dbContext;
            _dbSet=_db.Set<T>();
        }
        public void AddRepo(T entity)
        {
            _dbSet.Add(entity);
        }
        public bool AnyRepo(Expression<Func<T, bool>> filter = null)
        {
            return _dbSet.Any(filter);
        }

        public IEnumerable<T> GetAllRepo(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (!string.IsNullOrEmpty(includeProperties))
            {
                //Villa, VillaNumber --Case Sensitive and If you want to Include more, separate by comma
                foreach (var includePro in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includePro);
                }
            }
            return query.ToList();
        }

        public T GetRepo(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;
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

        public void RemoveRepo(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void UpdateRepo(T entity)
        {
            _db.Update(entity);
        }
    }
}
