using FamilyFinancialControl.Communication.ViewObjects.User;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyFinancialControl.Core.ServicesInterface;

public interface IUserService
{
    public Task<Result> SaveAsync(UserVO userVO);
    public Task<Result> UpdateAsync(UserVO userVO);
    public Task<Result> DeleteAsync(int id);
    public Task<Result<List<UserVO>>> GetAllAsync();
    public Task<Result<UserVO>> GetByIdAsync(int id);
}
