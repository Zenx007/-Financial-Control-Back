using AutoMapper;
using FamilyFinancialControl.Communication.ViewObjects.Category;
using FamilyFinancialControl.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyFinancialControl.Core.Profiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryVO>()
            .ReverseMap();
    }
}
