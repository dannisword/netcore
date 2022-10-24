using Microsoft.EntityFrameworkCore;

using DS.Common.Repository;

namespace DS.Order.Implement;
public class BaseService
{
    public IUnitOfWork UnitOfWork { get; private set; }
    public DbContext Context { get; private set; }
    public BaseService(IUnitOfWork unitOfWork)
    {
        this.UnitOfWork = unitOfWork;
        this.Context = this.UnitOfWork.DbContext;
    }


}