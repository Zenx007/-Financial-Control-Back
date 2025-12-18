using FamilyFinancialControl.Communication.ViewObjects.Category;
using FamilyFinancialControl.Communication.ViewObjects.Transaction;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyFinancialControl.Core.ServicesInterface;

public interface ITransactionService
{
    public Task<Result> SaveAsync(TransactionVO transactionVO);
    public Task<Result<TransactionVO>> GetByIdAsync(int id);
    public Task<Result> UpdateAsync(TransactionVO transactionVO);
    public Task<Result<List<TransactionVO>>> GetAllAsync();
    public Task<Result> DeleteAsync(int id);
    public Task<Result<TransactionTotalDetails>> GetTotalAsync();
    public Task<Result<List<TransactionVO>>> GetByUserIdAsync(int userId);
    public Task<Result<List<TransactionVO>>> GetByTypeAsync(int typeId);
    public Task<Result<List<TransactionVO>>> GetByCategoryAsync(int categoryId);
    public Task<Result<List<CategoryDetailsVO>>> GetCategoryDetailsAsync();
}