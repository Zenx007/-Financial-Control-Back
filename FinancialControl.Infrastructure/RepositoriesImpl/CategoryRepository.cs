using FamilyFinancialControl.Communication.ViewObjects.Category;
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

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _db;

    public CategoryRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<Category>> FindAllAsync()
    {
        try
        {
            List<Category> categories = await _db.Categories.ToListAsync();
            return categories;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<Category> FindByIdAsync(int id)
    {
        try
        {
            Category category = await _db.Categories
                .Where(x => x.Id == id)
                .Include(x => x.Transactions)
                .FirstOrDefaultAsync();

            return category;
        }
        catch (Exception)
        {
            return null; 
        }
    }
    public async Task<Result> InsertAsync(Category category)
    {
        try
        {
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
            return Result.Ok();
        }
        catch (Exception)
        {
            return Result.Fail(ConstantsMessagesCategory.ErrorSave);
        }
    }

    public async Task<Result> DeleteAsync(Category category)
    {
        try
        {
            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();

            return Result.Ok();
        }
        catch (Exception)
        {
            return Result.Fail(ConstantsMessagesCategory.ErrorDelete);
        }
    }

    public async Task<Result> UpdateAsync(Category category)
    {
        try
        {
            _db.Categories.Update(category);
            await _db.SaveChangesAsync();
            return Result.Ok();
        }
        catch (Exception)
        {
            return Result.Fail(ConstantsMessagesCategory.ErrorUpdate);
        }
    }
}
