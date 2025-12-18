using AutoMapper;
using FamilyFinancialControl.Communication.ViewObjects.Category;
using FamilyFinancialControl.Core.Entities;
using FamilyFinancialControl.Core.Enums;
using FamilyFinancialControl.Core.RepositoriesInterface;
using FamilyFinancialControl.Core.ServicesInterface;
using FamilyFinancialControl.Helpers;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyFinancialControl.Infrastructure.ServicesImpl;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepo;
    private readonly IMapper _mapper;
    private readonly ITransactionRepository _transactionRepo;

    public CategoryService(ICategoryRepository categoryRepo, IMapper mapper, ITransactionRepository transactionRepo)
    {
        _categoryRepo = categoryRepo;
        _mapper = mapper;
        _transactionRepo = transactionRepo;
    }

    public async Task<Result> SaveAsync(CategoryVO categoryVO)
    {
        try
        {
            Category category = _mapper.Map<Category>(categoryVO);

            Result result = await _categoryRepo.InsertAsync(category);
            if (result.IsFailed)
                return Result.Fail(ConstantsMessagesUser.ErrorSave);

            return Result.Ok();
        }
        catch (Exception)
        {
            return Result.Fail(ConstantsMessagesCategory.ErrorSave);
        }
    }
    public async Task<Result<List<CategoryVO>>> GetAllAsync()
    {
        try
        {
            List<Category> categories = await _categoryRepo.FindAllAsync();
            if (categories == null)
                return Result.Ok(new List<CategoryVO>());

            List<CategoryVO> vos = _mapper.Map<List<CategoryVO>>(categories);
            return Result.Ok(vos);
        }
        catch (Exception)
        {
            return Result.Fail(ConstantsMessagesCategory.ErrorGetAll);
        }
    }

    public async Task<Result> DeleteAsync(int id)
    {
        try
        {
            Category category = await _categoryRepo.FindByIdAsync(id);
            if (category == null)
                return Result.Fail(ConstantsMessagesCategory.ErrorNotFound);

            Result result = await _categoryRepo.DeleteAsync(category);
            if (result.IsFailed)
                return Result.Fail(ConstantsMessagesUser.ErrorDelete);

            return Result.Ok();
        }
        catch (Exception)
        {
            return Result.Fail(ConstantsMessagesCategory.ErrorDelete);
        }
    }

    public async Task<Result<CategoryVO>> GetByIdAsync(int id)
    {
        try
        {
            Category category = await _categoryRepo.FindByIdAsync(id);
            if (category == null)
                return Result.Fail(ConstantsMessagesCategory.ErrorNotFound);

            CategoryVO categoryVO = _mapper.Map<CategoryVO>(category);

            return Result.Ok(categoryVO);
        }
        catch (Exception)
        {
            return Result.Fail(ConstantsMessagesCategory.ErrorPrepare);
        }
    }
    public async Task<Result> UpdateCategory(CategoryVO categoryVO)
    {
        try
        {
            Category category = await _categoryRepo.FindByIdAsync(categoryVO.Id);
            if (category == null)
                return Result.Fail(ConstantsMessagesCategory.ErrorNotFound);

            TypeCategory actualType = category.TypeCategory;

            category.Description = categoryVO.Description;
            category.TypeCategory = categoryVO.TypeCategory;

            Result updateCategory = await _categoryRepo.UpdateAsync(category);
            if (updateCategory.IsFailed)
                return Result.Fail(ConstantsMessagesCategory.ErrorUpdate);

            if (actualType != categoryVO.TypeCategory && categoryVO.TypeCategory != TypeCategory.Both)
            {
                TypeTransaction newType = new TypeTransaction();

                if (categoryVO.TypeCategory == TypeCategory.Expense) 
                    newType = TypeTransaction.Expense;

                if (categoryVO.TypeCategory == TypeCategory.Revenue)
                    newType = TypeTransaction.Revenue;

                List<Transaction> transactions = new List<Transaction>();

                foreach (Transaction transaction in category.Transactions)
                {
                    transaction.TypeTransaction = newType;
                }

                Result result = await _transactionRepo.UpdateList(transactions);
                if (result.IsFailed)
                    return Result.Fail(ConstantsMessagesCategory.ErrorUpdate);
            }

            return Result.Ok();
        }
        catch (Exception)
        {
            return Result.Fail(ConstantsMessagesCategory.ErrorDelete);
        }
    }
}