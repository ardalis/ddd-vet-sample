using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using ClientPatientManagement.Core.Interfaces;

namespace ClientPatientManagement.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly CrudContext _context;
        private readonly DbSet<TEntity> _dbSet;
        public Repository()
        {

        }
        public Repository(CrudContext context)
        {
            this._context = context;
            this._dbSet = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> List()
        {
            return _dbSet.ToList();
        }

        public TEntity GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entityToDelete = _dbSet.Find(id);
            _dbSet.Remove(entityToDelete);
            _context.SaveChanges();
        }
    }


    // Sample code below

    public interface IAggregateRoot : IEntity { }

    public class Root : IAggregateRoot
    {
        public int Id
        {
            get { throw new NotImplementedException(); }
        }
    }

    public class RootRepository : IRepository<Root>
    {
        public IEnumerable<Root> List()
        {
            throw new NotImplementedException();
        }

        public Root GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Root entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Root entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }

    public class NonRoot : IEntity
    {
        public int Id
        {
            get { throw new NotImplementedException(); }
        }
    }

    public class ClientCode
    {
        public void Foo()
        {
            var result = new Repository<NonRoot>().GetById(1);
        }
    }
}
