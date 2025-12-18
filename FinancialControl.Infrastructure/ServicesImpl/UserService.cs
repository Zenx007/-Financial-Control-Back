using AutoMapper;
using FamilyFinancialControl.Communication.ViewObjects.User;
using FamilyFinancialControl.Core.Entities;
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

public class UserService : IUserService
{
    private readonly IUserRepository _userRepo;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepo, IMapper mapper)
    {
        _userRepo = userRepo;
        _mapper = mapper;
    }

    public async Task<Result> SaveAsync(UserVO userVO)
    {
        try
        {
            User user = _mapper.Map<User>(userVO);

            Result saveUser = await _userRepo.InsertAsync(user);
            if (saveUser.IsFailed)
                return Result.Fail(ConstantsMessagesUser.ErrorSave);

            return Result.Ok();
        }
        catch (Exception)
        {
            return Result.Fail(ConstantsMessagesUser.ErrorSave);
        }
    }

    public async Task<Result> UpdateAsync(UserVO userVO)
    {
        try
        {
            User user = await _userRepo.FindByIdAsync(userVO.Id);
            if (user == null) 
                return Result.Fail(ConstantsMessagesUser.ErrorNotFound);

            user.Name = userVO.Name;
            user.Age = userVO.Age;

            Result update = await _userRepo.UpdateAsync(user);
            if (update.IsFailed)
                return Result.Fail(ConstantsMessagesUser.ErrorUpdate);

            return Result.Ok();
        }
        catch (Exception)
        {
            return Result.Fail(ConstantsMessagesUser.ErrorUpdate);
        }
    }

    public async Task<Result> DeleteAsync(int id)
    {
        try
        {
            if (id <= 0)
              return Result.Fail(ConstantsMessagesUser.ErrorNotFound);

            User user = await _userRepo.FindByIdAsync(id);
            if (user == null)
                return Result.Fail(ConstantsMessagesUser.ErrorNotFound);

            Result delete = await _userRepo.DeleteByIdAsync(user);
                if (delete.IsFailed)
                    return Result.Fail(ConstantsMessagesUser.ErrorDelete);

            return Result.Ok();
        }
        catch (Exception)
        {
            return Result.Fail(ConstantsMessagesUser.ErrorDelete);
        }
    }

    public async Task<Result<List<UserVO>>> GetAllAsync()
    {
        try
        {
            List<User> users = await _userRepo.FindAllAsync();
            if (users == null) 
                return Result.Ok(new List<UserVO>());

            List<UserVO> userVOs = _mapper.Map<List<UserVO>>(users);
            return Result.Ok(userVOs);
        }
        catch (Exception)
        {
            return Result.Fail(ConstantsMessagesUser.ErrorGetAll);
        }
    }

    public async Task<Result<UserVO>> GetByIdAsync(int id)
    {
        try
        {
            User user = await _userRepo.FindByIdAsync(id);
            if (user == null) 
                return Result.Fail(ConstantsMessagesUser.ErrorNotFound);

            UserVO userVO = _mapper.Map<UserVO>(user);

            return Result.Ok(userVO);
        }
        catch (Exception)
        {
            return Result.Fail(ConstantsMessagesUser.Prepare);
        }
    }
}
