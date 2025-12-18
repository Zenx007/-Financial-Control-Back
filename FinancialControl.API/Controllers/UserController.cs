using FamilyFinancialControl.Communication.ViewObjects.API;
using FamilyFinancialControl.Communication.ViewObjects.User;
using FamilyFinancialControl.Core.ServicesInterface;
using FamilyFinancialControl.Helpers;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace FamilyFinancialControl.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Save - Método que cadastra um novo Usuário
    /// </summary>
    [HttpPost]
    [Route("Save")]
    public async Task<IActionResult> SaveAsync([FromBody] UserVO userVO)
    {
        APIResponse response = new APIResponse();
        try
        {
            Result result = await _userService.SaveAsync(userVO);

            if (result.IsFailed)
            {
                response.Success = false;
                response.Message = StaticMethods.ExtractResultMessage(result);
                response.Object = null;

                return StatusCode(StatusCodes.Status400BadRequest, response);
            }

            response.Success = true;
            response.Message = ConstantsMessagesUser.SuccessSaved;
            response.Object = null;

            return StatusCode(StatusCodes.Status201Created, response);
        }
        catch (Exception)
        {
            response.Success = false;
            response.Message = ConstantsMessagesUser.ErrorSave;
            response.Object = null;

            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    /// <summary>
    /// Update - Método que atualiza um Usuário existente
    /// </summary>
    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdateAsync([FromBody] UserVO userVO)
    {
        APIResponse response = new APIResponse();
        try
        {
            Result result = await _userService.UpdateAsync(userVO);

            if (result.IsFailed)
            {
                response.Success = false;
                response.Message = StaticMethods.ExtractResultMessage(result);
                response.Object = null;

                return StatusCode(StatusCodes.Status400BadRequest, response);
            }

            response.Success = true;
            response.Message = ConstantsMessagesUser.SuccessSaved;
            response.Object = null;

            return StatusCode(StatusCodes.Status200OK, response);
        }
        catch (Exception)
        {
            response.Success = false;
            response.Message = ConstantsMessagesUser.ErrorUpdate;
            response.Object = null;

            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    /// <summary>
    /// GetAll - Método que retorna todos os Usuários
    /// </summary>
    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAllAsync()
    {
        APIResponse response = new APIResponse();
        try
        {
            Result<List<UserVO>> result = await _userService.GetAllAsync();

            if (result.IsFailed)
            {
                response.Success = false;
                response.Message = ConstantsMessagesUser.ErrorGetAll;
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
            response.Message = ConstantsMessagesUser.ErrorGetAll;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    /// <summary>
    /// Prepare - Busca um usuário pelo Id
    /// </summary>
    [HttpGet]
    [Route("Prepare")]
    public async Task<IActionResult> PrepareAsync([FromQuery] int id)
    {
        APIResponse response = new APIResponse();
        try
        {
            Result<UserVO> result = await _userService.GetByIdAsync(id);

            if (result.IsFailed)
            {
                response.Success = false;
                response.Message = StaticMethods.ExtractResultMessage(result); 
                response.Object = null;

                return StatusCode(StatusCodes.Status404NotFound, response);
            }

            response.Success = true;
            response.Object = result.Value;
            return StatusCode(StatusCodes.Status200OK, response);
        }
        catch (Exception)
        {
            response.Success = false;
            response.Message = ConstantsMessagesUser.Prepare;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }

    /// <summary>
    /// Delete - Deleta um usuário pelo ID
    /// </summary>
    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> DeleteAsync([FromQuery] int id)
    {
        APIResponse response = new APIResponse();
        try
        {
            Result result = await _userService.DeleteAsync(id);

            if (result.IsFailed)
            {
                response.Success = false;
                response.Message = StaticMethods.ExtractResultMessage(result);
                response.Object = null;

                return StatusCode(StatusCodes.Status400BadRequest, response);
            }

            response.Success = true;
            response.Message = ConstantsMessagesUser.SuccessDelete;
            response.Object = null;

            return StatusCode(StatusCodes.Status200OK, response);
        }
        catch (Exception)
        {
            response.Success = false;
            response.Message = ConstantsMessagesUser.ErrorDelete;
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }
}
