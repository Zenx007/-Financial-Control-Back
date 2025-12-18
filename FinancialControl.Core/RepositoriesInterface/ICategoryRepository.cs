using FamilyFinancialControl.Communication.ViewObjects.Category;
using FamilyFinancialControl.Core.Entities;
using FamilyFinancialControl.Core.Enums;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyFinancialControl.Core.RepositoriesInterface;

public interface ICategoryRepository
{
    public Task<List<Category>> FindAllAsync();
    public Task<Result> InsertAsync(Category category);
    public Task<Category> FindByIdAsync(int id);
    public Task<Result> DeleteAsync(Category category);
    public Task<Result> UpdateAsync(Category category);
}
