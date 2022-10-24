using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DS.Common.Repository;

public interface IUnitOfWork : IDisposable
{
    DbContext DbContext { get; }

    void BeginTransaction();

    int SaveChanges();

    void Commit();

    int Commit(bool usingTransaction);

    void Rollback();

    IRepository<TEntity> Repository<TEntity>();
}