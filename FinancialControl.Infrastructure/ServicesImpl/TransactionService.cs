using AutoMapper;
using FamilyFinancialControl.Communication.ViewObjects.Category;
using FamilyFinancialControl.Communication.ViewObjects.Transaction;
using FamilyFinancialControl.Communication.ViewObjects.User;
using FamilyFinancialControl.Core.Entities;
using FamilyFinancialControl.Core.Enums;
using FamilyFinancialControl.Core.RepositoriesInterface;
using FamilyFinancialControl.Core.ServicesInterface;
using FamilyFinancialControl.Helpers;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace FamilyFinancialControl.Infrastructure.ServicesImpl;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepo;
    private readonly IUserRepository _userRepo;
    private readonly ICategoryRepository _categoryRepo;
    private readonly IMapper _mapper;

    public TransactionService(ITransactionRepository transactionRepo, IUserRepository userRepo, ICategoryRepository categoryRepo, IMapper mapper)
    {
        _transactionRepo = transactionRepo;
        _userRepo = userRepo;
        _categoryRepo = categoryRepo;
        _mapper = mapper;
    }
    private async Task<Result> ValidateRules(TransactionVO vo)
    {
        User user = await _userRepo.FindByIdAsync(vo.UserId);
        if (user == null)
            return Result.Fail(ConstantsMessagesUser.ErrorNotFound);

        if (user.Age < 18)
        {
            if (vo.TypeTransaction != TypeTransaction.Expense)
            {
                return Result.Fail(ConstantsMessagesTransaction.ErrorAgeRestriction);
            }
        }

        Category category = await _categoryRepo.FindByIdAsync(vo.CategoryId);
        if (category == null)
            return Result.Fail(ConstantsMessagesCategory.ErrorNotFound);

        if (category.TypeCategory != TypeCategory.Both)
        {
            int categoryTypeId = (int)category.TypeCategory;
            if (categoryTypeId != (int)vo.TypeTransaction)
            {
                return Result.Fail(ConstantsMessagesTransaction.ErrorCategoryMismatch);
            }
        }

        return Result.Ok();
    }

    public async Task<Result> SaveAsync(TransactionVO transactionVO)
    {
        try
        {
            Result validation = await ValidateRules(transactionVO);
            if (validation.IsFailed) 
                return validation;

            Transaction transaction = _mapper.Map<Transaction>(transactionVO);

            Result saveTransaction = await _transactionRepo.InsertAsync(transaction);
            if (saveTransaction.IsFailed)
                return Result.Fail(ConstantsMessagesTransaction.ErrorSave);

            return Result.Ok();
        }
        catch (Exception)
        {
            return Result.Fail(ConstantsMessagesTransaction.ErrorSave);
        }
    }
    public async Task<Result<List<TransactionVO>>> GetAllAsync()
    {
        try
        {
            List<Transaction> transactions = await _transactionRepo.FindAllAsync();
            if (transactions == null)
                return Result.Ok(new List<TransactionVO>());

            List<TransactionVO> vos = _mapper.Map<List<TransactionVO>>(transactions);
            return Result.Ok(vos);
        }
        catch (Exception)
        {
            return Result.Fail(ConstantsMessagesTransaction.ErrorGetAll);
        }
    }

    public async Task<Result> UpdateAsync (TransactionVO transactionVO)
    {
        try
        {
            Transaction transaction = await _transactionRepo.FindByIdAsync(transactionVO.Id);
            if(transaction == null)
                return Result.Fail(ConstantsMessagesTransaction.ErrorNotFound);

            Result validation = await ValidateRules(transactionVO);
            if (validation.IsFailed)
                return validation;

            transaction.Description = transactionVO.Description;
            transaction.UserId = transactionVO.UserId;
            transaction.CategoryId = transactionVO.CategoryId;
            transaction.TypeTransaction = transactionVO.TypeTransaction;
            transaction.Value = transactionVO.Value;

            Result result = await _transactionRepo.UpdateAsync(transaction);
            if(result.IsFailed)
                return Result.Fail(ConstantsMessagesTransaction.ErrorUpdate);

            return Result.Ok();
        }
        catch (Exception)
        {
            return Result.Fail(ConstantsMessagesTransaction.ErrorUpdate);
        }
    }

    public async Task<Result<TransactionVO>> GetByIdAsync(int id)
    {
        try
        {
            Transaction transaction = await _transactionRepo.FindByIdAsync(id);
            if (transaction == null)
                return Result.Fail(ConstantsMessagesTransaction.ErrorNotFound);

            TransactionVO transactionVO = _mapper.Map<TransactionVO>(transaction);

            return Result.Ok(transactionVO);
        }
        catch (Exception)
        {
            return Result.Fail(ConstantsMessagesTransaction.ErrorPrepare);
        }
    }

    public async Task<Result> DeleteAsync(int id)
    {
        try
        {
            Transaction transaction = await _transactionRepo.FindByIdAsync(id);
            if (transaction == null)
                return Result.Fail(ConstantsMessagesTransaction.ErrorNotFound);

            Result result = await _transactionRepo.DeleteByIdAsync(id);
            if (result.IsFailed)
                return Result.Fail(ConstantsMessagesTransaction.ErrorDelete);

            return Result.Ok();
        }
        catch (Exception)
        {
            return Result.Fail(ConstantsMessagesTransaction.ErrorDelete);
        }
    }
    public async Task<Result<TransactionTotalDetails>> GetTotalAsync()
    {
        try
        {
            List<UserDetailsVO> userDetailsList = await _userRepo.FindUsersDetailsAsync();
            if (userDetailsList == null)
                return Result.Ok(new TransactionTotalDetails());

            TransactionTotalDetails response = new TransactionTotalDetails();

            response.UserDetails = userDetailsList;

            foreach (UserDetailsVO user in response.UserDetails)
            {
                user.Balance = user.Revenue - user.Expense;

                user.TransactionNumberTotal = user.RevenueCount + user.ExpenseCount;

                response.ExpenseCount += user.ExpenseCount;

                response.ExpenseTotal += + user.Expense;

                response.RevenueTotal += user.Revenue;

                response.RevenueCount += user.RevenueCount;

                response.TransactionNumberTotal += user.TransactionNumberTotal;
            }

            response.BalanceTotal = response.RevenueTotal - response.ExpenseTotal;

            return Result.Ok(response);
        }
        catch (Exception)
        {
            return Result.Fail(ConstantsMessagesTransaction.ErrorGetTotals);
        }
    }

    public async Task<Result<List<TransactionVO>>> GetByUserIdAsync(int userId)
    {
        try
        {
            if (userId <= 0)
                return Result.Fail(ConstantsMessagesUser.ErrorNotFound);

            List<Transaction> list = await _transactionRepo.FindByUserIdAsync(userId);
            if (list == null)
                return Result.Ok(new List<TransactionVO>());

            List<TransactionVO> response = _mapper.Map<List<TransactionVO>>(list);

            return Result.Ok(response);
        }
        catch (Exception)
        {
            return Result.Fail(ConstantsMessagesTransaction.ErrorGetAll);
        }
    }

    public async Task<Result<List<TransactionVO>>> GetByTypeAsync(int typeId)
    {
        try
        {
            if (typeId <= 0)
                return Result.Fail(ConstantsMessagesCategory.ErrorNotFound);

            List<Transaction> transactions = await _transactionRepo.FindByTypeAsync(typeId);
            if (transactions == null)
                return Result.Ok(new List<TransactionVO>());

            List<TransactionVO> vos = _mapper.Map<List<TransactionVO>>(transactions);

            return Result.Ok(vos);
        }
        catch (Exception)
        {
            return Result.Fail(ConstantsMessagesTransaction.ErrorGetAll);
        }
    }

    public async Task<Result<List<TransactionVO>>> GetByCategoryAsync(int categoryId)
    {
        try
        {
            if (categoryId <= 0)
                return Result.Fail(ConstantsMessagesCategory.ErrorNotFound);

            List<Transaction> transactions = await _transactionRepo.FindByCategoryAsync(categoryId);
            if (transactions == null)
                return Result.Ok(new List<TransactionVO>());

            List<TransactionVO> vos = _mapper.Map<List<TransactionVO>>(transactions);

            return Result.Ok(vos);
        }
        catch (Exception)
        {
            return Result.Fail(ConstantsMessagesTransaction.ErrorGetAll);
        }
    }
    public async Task<Result<List<CategoryDetailsVO>>> GetCategoryDetailsAsync()
    {
        try
        {
            List<CategoryDetailsVO> details = await _transactionRepo.FindCategoryDetailsAsync();
            if (details == null)
                return Result.Ok(new List<CategoryDetailsVO>());

            return Result.Ok(details);
        }
        catch (Exception)
        {
            return Result.Fail(ConstantsMessagesCategory.ErrorGetAllDetails);
        }
    }
}