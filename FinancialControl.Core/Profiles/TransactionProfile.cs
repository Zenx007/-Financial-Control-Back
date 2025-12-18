using AutoMapper;
using FamilyFinancialControl.Communication.ViewObjects.Transaction;
using FamilyFinancialControl.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyFinancialControl.Core.Profiles;

public class TransactionProfile : Profile
{
    public TransactionProfile()
    {
        CreateMap<Transaction, TransactionVO>()
            .ReverseMap();
    }
}
