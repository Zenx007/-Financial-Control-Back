using FamilyFinancialControl.Communication.ViewObjects.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyFinancialControl.Communication.ViewObjects.Transaction;

public class TransactionTotalDetails
{
    public List<UserDetailsVO> UserDetails { get; set; }
    public decimal ExpenseTotal { get; set; }
    public int ExpenseCount { get; set; }
    public int RevenueCount { get; set; }
    public decimal RevenueTotal { get; set; }
    public int TransactionNumberTotal { get; set; }
    public decimal BalanceTotal { get; set; }
}
