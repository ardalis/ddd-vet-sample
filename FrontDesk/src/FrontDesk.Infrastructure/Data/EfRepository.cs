using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using FrontDesk.Core.Aggregates;
using FrontDesk.SharedKernel;
using FrontDesk.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontDesk.Infrastructure.Data
{
    public class EfRepository : IRepository
    {
        private readonly AppDbContext _dbContext;

        public EfRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public T GetById<T, TId>(TId id) where T : BaseEntity<TId>, IAggregateRoot
        {
            return _dbContext.Set<T>().SingleOrDefault(e => e.Id.Equals(id));
        }

        public Task<T> GetByIdAsync<T, TId>(TId id) where T : BaseEntity<TId>, IAggregateRoot
        {
            return _dbContext.Set<T>().SingleOrDefaultAsync(e => e.Id.Equals(id));
        }

        public Task<List<T>> ListAsync<T, TId>() where T : BaseEntity<TId>, IAggregateRoot
        {
            return _dbContext.Set<T>().ToListAsync();
        }

        public async Task<List<T>> ListAsync<T, TId>(ISpecification<T> spec) where T : BaseEntity<TId>, IAggregateRoot
        {
            var specificationResult = ApplySpecification<T, TId>(spec);
            return await specificationResult.ToListAsync();
        }

        public async Task<T> AddAsync<T, TId>(T entity) where T : BaseEntity<TId>, IAggregateRoot
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateAsync<T, TId>(T entity) where T : BaseEntity<TId>, IAggregateRoot
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync<T, TId>(T entity) where T : BaseEntity<TId>, IAggregateRoot
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        private IQueryable<T> ApplySpecification<T, TId>(ISpecification<T> spec) where T : BaseEntity<TId>
        {
            var evaluator = new SpecificationEvaluator<T>();
            return evaluator.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
        }
    }
}
