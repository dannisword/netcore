using DS.Common.Entities;

namespace DS.Order.Interface;

public interface IUserService
{
    IEnumerable<dynamic> Reads();

    int Add(UserAccount user);

    int Update(UserAccount user);

    int Delete(UserAccount user);
}