using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DS.Common.Repository;
public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly IUnitOfWork unitOfWork;
    private readonly DbContext dbContext;
    private readonly DbSet<TEntity> dbSet;
    public Repository(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
        this.dbContext = unitOfWork.DbContext;
        this.dbSet = this.dbContext.Set<TEntity>();
    }
    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
    {
        return this.dbContext.Set<TEntity>().Where(predicate);
    }
    public IEnumerable<TEntity> FindAll()
    {
        return this.dbContext.Set<TEntity>();
    }
    public TEntity Add(TEntity entity)
    {
        this.dbSet.Add(entity);
        return entity;
    }
    public void Update(TEntity entity)
    {
        this.dbContext.Entry(entity).State = EntityState.Modified;
    }
    public void Delete(TEntity entity)
    {
        this.dbSet.Remove(entity);
    }
    public IQueryable<TEntity> Queryable()
    {
        return this.dbSet;
    }

    public IRepository<TEntity> GetRepository()
    {
        return this.unitOfWork.Repository<TEntity>();
    }
}