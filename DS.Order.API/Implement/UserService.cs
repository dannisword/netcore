
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

using DS.Common;
using DS.Common.Entities;
using DS.Common.Repository;
using DS.Order.Interface;

namespace DS.Order.Implement;

public class UserService : BaseService, IUserService
{
    public UserService(IUnitOfWork unitOfWork)
   : base(unitOfWork)
    {
    }

    public IEnumerable<dynamic> Reads()
    {
        var predicate = PredicateBuilder.True<UserAccount>();
        //predicate = predicate.And(p => p.IsActive == true);
        /*
         if (query.ID > 0)
         {
             predicate = predicate.And(p => p.UserID == query.ID);
         }
         if (string.IsNullOrEmpty(query.ShortName) == false)
         {
             predicate = predicate.And(p => p.ShortName.Contains(query.ShortName));
         }
 */
        return this.UnitOfWork.Repository<UserAccount>().Find(predicate);

    }

    public int Add(UserAccount user)
    {
        //Expression<Func<UserAccount, bool>> expression = (p) => p.UserID == user.UserID;
        //var data = this.UnitOfWork.Repository<UserAccount>().Find(expression);

        var predicate = PredicateBuilder.True<UserAccount>();
        predicate = predicate.And(p => p.IsActive == true);
        predicate = predicate.And(p => p.UserID == user.UserID);
        var data = this.UnitOfWork.Repository<UserAccount>().Find(predicate);
        // Add
        try
        {
            if (data.Any() == false)
            {
                this.UnitOfWork.Repository<UserAccount>().Add(user);
                return this.UnitOfWork.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            var msg = ex.Message;
        }

        return 0;
    }

    public int Update(UserAccount user)
    {
        throw new Exception();
    }

    public int Delete(UserAccount user)
    {
        throw new Exception();
    }

    private IEnumerable<UserAccount> find(UserAccount user)
    {
        Expression<Func<UserAccount, bool>> expression = (p) => p.UserID == user.UserID;

        return this.UnitOfWork.Repository<UserAccount>().Find(expression);
    }
}