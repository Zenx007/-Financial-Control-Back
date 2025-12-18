using FamilyFinancialControl.Communication.ViewObjects.User;
using FamilyFinancialControl.Core.Entities;
using FamilyFinancialControl.Core.Enums;
using FamilyFinancialControl.Core.RepositoriesInterface;
using FamilyFinancialControl.Helpers;
using FamilyFinancialControl.Infrastructure.Data.Context;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyFinancialControl.Infrastructure.RepositoriesImpl;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<User>> FindAllAsync()
    {
        try
        {
            List<User> users = await _db.Users.ToListAsync();
            return users;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<User> FindByIdAsync(int id)
    {
        try
        {
            User user = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<Result> InsertAsync(User user)
    {
        try
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return Result.Ok();
        }
        catch (Exception)
        {
            return Result.Fail(ConstantsMessagesUser.ErrorSave);
        }
    }

    public async Task<Result> UpdateAsync(User user)
    {
        try
        {
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
            return Result.Ok();
        }
        catch (Exception)
        {
            return Result.Fail(ConstantsMessagesUser.ErrorUpdate);
        }
    }

    public async Task<Result> DeleteByIdAsync(User user)
    {
        try
        {
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
            return Result.Ok();
        }
        catch (Exception)
        {
            return Result.Fail(ConstantsMessagesUser.ErrorDelete);
        }
    }

    public async Task<List<UserDetailsVO>> FindUsersDetailsAsync()
    {
        try
        {
            List<UserDetailsVO> result = await _db.Users
                .Select(x => new UserDetailsVO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Expense = (decimal)x.Transactions
                        .Where(x => x.TypeTransaction == TypeTransaction.Expense)
                        .Sum(t => (double)t.Value),
                    ExpenseCount = x.Transactions
                        .Count(x => x.TypeTransaction == TypeTransaction.Expense),
                    Revenue = (decimal)x.Transactions
                        .Where(t => t.TypeTransaction == TypeTransaction.Revenue)
                        .Sum(x => (double)x.Value),
                    RevenueCount = x.Transactions
                        .Count(x => x.TypeTransaction == TypeTransaction.Revenue)
                }).ToListAsync();

            return result;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
