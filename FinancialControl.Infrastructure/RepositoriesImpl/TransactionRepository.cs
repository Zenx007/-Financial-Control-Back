using FamilyFinancialControl.Communication.ViewObjects.Category;
using FamilyFinancialControl.Communication.ViewObjects.Transaction;
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

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _db;

    public TransactionRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<Transaction>> FindAllAsync()
    {
        try
        {
            List<Transaction> transactions = await _db.Transactions
                .Include(x => x.Category)
                .Include(x => x.User)
                .ToListAsync();
            return transactions;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<Transaction> FindByIdAsync(int id)
    {
        try
        {
            Transaction transaction = await _db.Transactions
                .Include(x => x.Category)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
            return transaction;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<Result> InsertAsync(Transaction transaction)
    {
        try
        {
            _db.Transactions.Add(transaction);
            await _db.SaveChangesAsync();
            return Result.Ok();
        }
        catch (Exception)
        {
            return Result.Fail(ConstantsMessagesTransaction.ErrorSave);
        }
    }
    public async Task<Result> DeleteByIdAsync(int id)
    {
        try
        {
            Transaction transaction = await FindByIdAsync(id);
            if (transaction == null) return Result.Fail(ConstantsMessagesTransaction.ErrorNotFound);

            _db.Transactions.Remove(transaction);
            await _db.SaveChangesAsync();
            return Result.Ok();
        }
        catch (Exception)
        {
            return Result.Fail(ConstantsMessagesTransaction.ErrorDelete);
        }
    }

    public async Task<List<Transaction>> FindByUserIdAsync(int userId)
    {
        try
        {
            List<Transaction> transactions = await _db.Transactions
                .Where(x => x.UserId == userId)
                .AsNoTracking()
                .ToListAsync();

            return transactions;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<List<Transaction>> FindByTypeAsync(int typeId)
    {
        try
        {
            List<Transaction> list = await _db.Transactions
            .Include(t => t.User)
            .Include(t => t.Category)
            .Where(t => (int)t.TypeTransaction == typeId)
            .OrderByDescending(t => t.Id)
            .ToListAsync();

            return list;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<List<Transaction>> FindByCategoryAsync(int categoryId)
    {
        try
        {
            List<Transaction> list = await _db.Transactions
            .Include(t => t.User)
            .Include(t => t.Category)
            .Where(t => (int)t.CategoryId == categoryId)
            .OrderByDescending(t => t.Id)
            .ToListAsync();

            return list;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<List<CategoryDetailsVO>> FindCategoryDetailsAsync()
    {
        try
        {
            List<CategoryDetailsVO> result = await _db.Categories
                .Select(x => new CategoryDetailsVO
                {
                    Id = x.Id,
                    CategoryName = x.Description,
                    Expense = (decimal)x.Transactions
                        .Where(x => x.TypeTransaction == TypeTransaction.Expense)
                        .Sum(x => (double)x.Value),

                    ExpenseCount = x.Transactions
                    .Count(x => x.TypeTransaction == TypeTransaction.Expense),

                    Revenue = (decimal)x.Transactions
                        .Where(x => x.TypeTransaction == TypeTransaction.Revenue)
                        .Sum(x => (double)x.Value),

                    RevenueCount = x.Transactions
                    .Count(x => x.TypeTransaction == TypeTransaction.Revenue),

                    TransactionCount = x.Transactions.Count()
                }).ToListAsync();

            return result;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<Result> UpdateAsync(Transaction transaction)
    {
        try
        {
            _db.Transactions.Update(transaction);
            await _db.SaveChangesAsync();
            return Result.Ok();
        }
        catch (Exception)
        {
            return Result.Fail(ConstantsMessagesTransaction.ErrorDelete);
        }
    }

    public async Task<Result> UpdateList(List<Transaction> transactions)
    {
        try
        {
            _db.UpdateRange(transactions);
            await _db.SaveChangesAsync();

            return Result.Ok();
        }
        catch
        {
            return Result.Fail(ConstantsMessagesTransaction.ErrorUpdate);
        }
    }
}