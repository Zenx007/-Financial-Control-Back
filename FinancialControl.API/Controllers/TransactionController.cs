using FamilyFinancialControl.Communication.ViewObjects.API;
using FamilyFinancialControl.Communication.ViewObjects.Category;
using FamilyFinancialControl.Communication.ViewObjects.Transaction;
using FamilyFinancialControl.Communication.ViewObjects.User;
using FamilyFinancialControl.Core.ServicesInterface;
using FamilyFinancialControl.Helpers;
using FamilyFinancialControl.Infrastructure.ServicesImpl;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FamilyFinancialControl.API.Controllers;


[Route("[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    /// <summary>
    ///  Save - Método que salva uma nova transação
    /// </summary>
    [HttpPost]
    [Route("Save")]
    public async Task<IActionResult> SaveAsync([FromBody] TransactionVO transactionVO)
    {
        APIResponse response = new APIResponse();
        try
        {
            Result result = await _transactionService.SaveAsync(transactionVO);

            if (result.IsFailed)
            {
                response.Success = false;
                response.Message = StaticMethods.ExtractResultMessage(result);
                response.Object = null;
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }

            response.Success = true;
            response.Message = ConstantsMessagesTransaction.SuccessSaved;
            response.Object = null;
            return StatusCode(StatusCodes.Status201Created, response);
        }
        catch (Exception)
        {
            response.Success = false;
            response.Message = ConstantsMessagesTransaction.ErrorSave;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    /// <summary>
    ///  Prepare - Método que prepara os dados de uma transação
    /// </summary>
    [HttpGet]
    [Route("Prepare")]
    public async Task<IActionResult> PrepareAsync([FromQuery] int id)
    {
        APIResponse response = new APIResponse();
        try
        {
            Result<TransactionVO> result = await _transactionService.GetByIdAsync(id);

            if (result.IsFailed)
            {
                response.Success = false;
                response.Message = ConstantsMessagesTransaction.ErrorPrepare;
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }

            response.Success = true;
            response.Object = result.Value;
            return StatusCode(StatusCodes.Status200OK, response);
        }
        catch (Exception)
        {
            response.Success = false;
            response.Message = ConstantsMessagesTransaction.ErrorPrepare;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    /// <summary>
    ///  Delete - Método que deleta uma transação
    /// </summary>
    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> DeleteAsync([FromQuery] int id)
    {
        APIResponse response = new APIResponse();
        try
        {
            Result result = await _transactionService.DeleteAsync(id);

            if (result.IsFailed)
            {
                response.Success = false;
                response.Message = ConstantsMessagesTransaction.ErrorDelete;
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }

            response.Success = true;
            response.Object = null;
            response.Message = ConstantsMessagesTransaction.SuccessDelete;
            return StatusCode(StatusCodes.Status200OK, response);
        }
        catch (Exception)
        {
            response.Success = false;
            response.Message = ConstantsMessagesTransaction.ErrorDelete;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    /// <summary>
    ///  Update - Método que atualiza uma transação
    /// </summary>
    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdateAsync([FromBody] TransactionVO categoryVO)
    {
        APIResponse response = new APIResponse();
        try
        {
            Result result = await _transactionService.UpdateAsync(categoryVO);

            if (result.IsFailed)
            {
                response.Success = false;
                response.Message = ConstantsMessagesTransaction.ErrorUpdate;
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }

            response.Success = true;
            response.Object = null;
            response.Message = ConstantsMessagesTransaction.SuccessUpdate;
            return StatusCode(StatusCodes.Status200OK, response);
        }
        catch (Exception)
        {
            response.Success = false;
            response.Message = ConstantsMessagesTransaction.ErrorUpdate;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    /// <summary>
    ///  GetAll - Método que lista todas as transações
    /// </summary>
    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAllAsync()
    {
        APIResponse response = new APIResponse();
        try
        {
            Result<List<TransactionVO>> result = await _transactionService.GetAllAsync();

            if (result.IsFailed)
            {
                response.Success = false;
                response.Message = ConstantsMessagesTransaction.ErrorGetAll;
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }

            response.Success = true;
            response.Object = result.Value;
            return StatusCode(StatusCodes.Status200OK, response);
        }
        catch (Exception)
        {
            response.Success = false;
            response.Message = ConstantsMessagesTransaction.ErrorGetAll;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    /// <summary>
    ///  GetTotal - Método que lista os detalhes das transações de todos os usuários
    /// </summary>
    [HttpGet]
    [Route("GetTotal")]
    public async Task<IActionResult> GetTotalAsync()
    {
        APIResponse response = new APIResponse();
        try
        {
            Result<TransactionTotalDetails> result = await _transactionService.GetTotalAsync();

            if (result.IsFailed)
            {
                response.Success = false;
                response.Message = StaticMethods.ExtractResultMessage(result);
                response.Object = null;

                return StatusCode(StatusCodes.Status400BadRequest, response);
            }

            response.Success = true;
            response.Object = result.Value;

            return StatusCode(StatusCodes.Status200OK, response);
        }
        catch (Exception)
        {
            response.Success = false;
            response.Message = ConstantsMessagesTransaction.ErrorGetTotals;

            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    /// <summary>
    /// GetByUserId - Método que lista os detalhes das transações de um usuário
    /// </summary>
    [HttpGet]
    [Route("GetByUserId")]
    public async Task<IActionResult> GetByUserIdAsync([FromQuery] int userId)
    {
        APIResponse response = new APIResponse();
        try
        {
            Result<List<TransactionVO>> result = await _transactionService.GetByUserIdAsync(userId);

            if (result.IsFailed)
            {
                response.Success = false;
                response.Message = StaticMethods.ExtractResultMessage(result);
                response.Object = null;

                return StatusCode(StatusCodes.Status400BadRequest, response);
            }

            response.Success = true;
            response.Object = result.Value;

            return StatusCode(StatusCodes.Status200OK, response);
        }
        catch (Exception)
        {
            response.Success = false;
            response.Message = ConstantsMessagesTransaction.ErrorGetAll;

            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    /// <summary>
    /// GetByType - Método que lista os detalhes das transações de um tipo de transação
    /// </summary>
    [HttpGet("GetByType")]
    public async Task<IActionResult> GetByType([FromQuery] int typeId)
    {
        APIResponse response = new APIResponse();
        try
        {
            Result<List<TransactionVO>> result = await _transactionService.GetByTypeAsync(typeId);

            if (result.IsFailed)
            {
                response.Success = false;
                response.Message = StaticMethods.ExtractResultMessage(result);
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }

            response.Success = true;
            response.Object = result.Value;
            return StatusCode(StatusCodes.Status200OK, response);
        }
        catch (Exception)
        {
            response.Success = false;
            response.Message = ConstantsMessagesTransaction.ErrorGetAll;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    /// <summary>
    /// GetByCategory - Método que lista os detalhes das transações de uma categoria
    /// </summary>
    [HttpGet("GetByCategory")]
    public async Task<IActionResult> GetByCategoryAsync([FromQuery] int categoryId)
    {
        APIResponse response = new APIResponse();
        try
        {
            Result<List<TransactionVO>> result = await _transactionService.GetByCategoryAsync(categoryId);

            if (result.IsFailed)
            {
                response.Success = false;
                response.Message = StaticMethods.ExtractResultMessage(result);
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }

            response.Success = true;
            response.Object = result.Value;
            return StatusCode(StatusCodes.Status200OK, response);
        }
        catch (Exception)
        {
            response.Success = false;
            response.Message = ConstantsMessagesTransaction.ErrorGetAll;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    /// <summary>
    /// GetTotal - Traz os detalhes das transações por categoria
    /// </summary>
    [HttpGet]
    [Route("GetTotalCategory")]
    public async Task<IActionResult> GetTotalCategoryAsync()
    {
        APIResponse response = new APIResponse();
        try
        {
            Result<List<CategoryDetailsVO>> result = await _transactionService.GetCategoryDetailsAsync();

            if (result.IsFailed)
            {
                response.Success = false;
                response.Message = StaticMethods.ExtractResultMessage(result);
                response.Object = null;

                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }

            response.Success = true;
            response.Object = result.Value;

            return StatusCode(StatusCodes.Status200OK, response);
        }
        catch (Exception)
        {
            response.Success = false;
            response.Message = ConstantsMessagesTransaction.ErrorGetAll;

            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }
}
