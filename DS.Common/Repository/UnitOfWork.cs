using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DS.Common.Repository;
public class UnitOfWork : IUnitOfWork
{
    private bool disposed = false;
    private Dictionary<string, dynamic>? repositories;
    public DbContext DbContext { get; private set; }

    public UnitOfWork(DbContext dbContext)
    {
        this.DbContext = dbContext;
    }
    public void BeginTransaction()
    {
        this.DbContext.Database.BeginTransaction();
    }
    public int SaveChanges()
    {
        return this.DbContext.SaveChanges();
    }
    public void Commit()
    {
        this.DbContext.Database.CommitTransaction();
    }
    public int Commit(bool usingTransaction)
    {
        if (!usingTransaction)
        {
            this.SaveChanges();
        }
        int status = -1;
        using (var trans = this.DbContext.Database.BeginTransaction())
        {
            try
            {
                status = this.DbContext.SaveChanges();
                trans.Commit();
            }
            catch (Exception)
            {
                trans.Rollback();
                throw;
            }
        }
        return status;
    }
    public void Rollback()
    {
        this.DbContext.Database.RollbackTransaction();
    }
    public IRepository<TEntity> Repository<TEntity>()
    {
        if (repositories == null)
        {
            repositories = new Dictionary<string, dynamic>();
        }

        var type = typeof(TEntity).Name;
        if (repositories.ContainsKey(type))
        {
            return (IRepository<TEntity>)repositories[type];
        }

        var repositoryType = typeof(Repository<>);

        repositories.Add(type, Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), this));
        return repositories[type];
    }


    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                this.DbContext.Dispose();
                this.DbContext = null;
            }
        }
        this.disposed = true;
    }
}
