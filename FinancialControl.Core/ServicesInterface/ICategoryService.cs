using FamilyFinancialControl.Communication.ViewObjects.Category;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyFinancialControl.Core.ServicesInterface;

public interface ICategoryService
{
    public Task<Result> SaveAsync(CategoryVO categoryVO);
    public Task<Result<List<CategoryVO>>> GetAllAsync();
    public Task<Result> DeleteAsync(int id);
    public Task<Result> UpdateCategory(CategoryVO categoryVO);
    public Task<Result<CategoryVO>> GetByIdAsync(int id);
}