using FamilyFinancialControl.Communication.ViewObjects.User;
using FamilyFinancialControl.Core.Entities;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyFinancialControl.Core.RepositoriesInterface;

public interface IUserRepository
{
    public Task<List<User>> FindAllAsync();
    public Task<User> FindByIdAsync(int id);
    public Task<Result> InsertAsync(User user);
    public Task<Result> UpdateAsync(User user);
    public Task<Result> DeleteByIdAsync(User user);
    public Task<List<UserDetailsVO>> FindUsersDetailsAsync();
}
