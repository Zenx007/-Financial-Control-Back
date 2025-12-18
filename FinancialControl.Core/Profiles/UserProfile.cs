using AutoMapper;
using FamilyFinancialControl.Communication.ViewObjects.User;
using FamilyFinancialControl.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyFinancialControl.Core.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserVO>()
            .ReverseMap();
    }
}
