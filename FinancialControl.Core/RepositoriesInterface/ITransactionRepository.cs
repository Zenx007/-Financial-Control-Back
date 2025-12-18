using FamilyFinancialControl.Communication.ViewObjects.Category;
using FamilyFinancialControl.Communication.ViewObjects.Transaction;
using FamilyFinancialControl.Core.Entities;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyFinancialControl.Core.RepositoriesInterface;

public interface ITransactionRepository
{
    public Task<List<Transaction>> FindAllAsync();
    public Task<Result> InsertAsync(Transaction transaction);
    public Task<Transaction> FindByIdAsync(int id);
    public Task<Result> DeleteByIdAsync(int id);
    public Task<Result> UpdateAsync (Transaction transaction);
    public Task<List<Transaction>> FindByUserIdAsync(int userId);
    public Task<List<Transaction>> FindByTypeAsync(int typeId);
    public Task<List<Transaction>> FindByCategoryAsync(int categoryId);
    public Task<List<CategoryDetailsVO>> FindCategoryDetailsAsync();
    public Task<Result> UpdateList(List<Transaction> transactions);
}
