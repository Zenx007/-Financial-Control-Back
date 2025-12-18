using FamilyFinancialControl.Communication.ViewObjects.API;
using FamilyFinancialControl.Communication.ViewObjects.Category;
using FamilyFinancialControl.Communication.ViewObjects.Transaction;
using FamilyFinancialControl.Core.ServicesInterface;
using FamilyFinancialControl.Helpers;
using FamilyFinancialControl.Infrastructure.ServicesImpl;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace FamilyFinancialControl.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    /// <summary>
    ///  Save - Método que salva uma nova categoria
    /// </summary>
    [HttpPost]
    [Route("Save")]
    public async Task<IActionResult> SaveAsync([FromBody] CategoryVO categoryVO)
    {
        APIResponse response = new APIResponse();
        try
        {
            Result result = await _categoryService.SaveAsync(categoryVO);

            if (result.IsFailed)
            {
                response.Success = false;
                response.Message = StaticMethods.ExtractResultMessage(result);
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }

            response.Success = true;
            response.Message = ConstantsMessagesCategory.SuccessSaved;
            return StatusCode(StatusCodes.Status201Created, response);
        }
        catch (Exception)
        {
            response.Success = false;
            response.Message = ConstantsMessagesCategory.ErrorSave;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    /// <summary>
    ///  Delete - Método que deleta uma categoria
    /// </summary>
    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> DeleteAsync([FromQuery] int id)
    {
        APIResponse response = new APIResponse();
        try
        {
            Result result = await _categoryService.DeleteAsync(id);

            if (result.IsFailed)
            {
                response.Success = false;
                response.Message = ConstantsMessagesCategory.ErrorDelete;
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }

            response.Success = true;
            response.Object = null;
            response.Message = ConstantsMessagesCategory.SuccessDelete;
            return StatusCode(StatusCodes.Status200OK, response);
        }
        catch (Exception)
        {
            response.Success = false;
            response.Message = ConstantsMessagesCategory.ErrorDelete;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    /// <summary>
    ///  Update - Método que atualiza uma categoria
    /// </summary>
    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdateAsync([FromBody] CategoryVO categoryVO)
    {
        APIResponse response = new APIResponse();
        try
        {
            Result result = await _categoryService.UpdateCategory(categoryVO);

            if (result.IsFailed)
            {
                response.Success = false;
                response.Message = ConstantsMessagesCategory.ErrorUpdate;
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }

            response.Success = true;
            response.Object = null;
            response.Message = ConstantsMessagesCategory.SuccessUpdate;
            return StatusCode(StatusCodes.Status200OK, response);
        }
        catch (Exception)
        {
            response.Success = false;
            response.Message = ConstantsMessagesCategory.ErrorUpdate;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    /// <summary>
    ///  Prepare - Método que prepara os dados de uma categoria
    /// </summary>
    [HttpGet]
    [Route("Prepare")]
    public async Task<IActionResult> PrepareAsync([FromQuery] int id)
    {
        APIResponse response = new APIResponse();
        try
        {
            Result<CategoryVO> result = await _categoryService.GetByIdAsync(id);

            if (result.IsFailed)
            {
                response.Success = false;
                response.Message = ConstantsMessagesCategory.ErrorPrepare;
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }

            response.Success = true;
            response.Object = result.Value;
            return StatusCode(StatusCodes.Status200OK, response);
        }
        catch (Exception)
        {
            response.Success = false;
            response.Message = ConstantsMessagesCategory.ErrorPrepare;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    /// <summary>
    ///  GetAll - Método que lista todas as categorias
    /// </summary>
    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAllAsync()
    {
        APIResponse response = new APIResponse();
        try
        {
            Result<List<CategoryVO>> result = await _categoryService.GetAllAsync();

            if (result.IsFailed)
            {
                response.Success = false;
                response.Message = ConstantsMessagesCategory.ErrorGetAll;
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }

            response.Success = true;
            response.Object = result.Value;
            return StatusCode(StatusCodes.Status200OK, response);
        }
        catch (Exception)
        {
            response.Success = false;
            response.Message = ConstantsMessagesCategory.ErrorGetAll;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }
}
