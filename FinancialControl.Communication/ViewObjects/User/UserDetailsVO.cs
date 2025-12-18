using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyFinancialControl.Communication.ViewObjects.User;

public class UserDetailsVO
{
  public int Id { get; set; }
  public string Name { get; set; }
  public int ExpenseCount { get; set; }
  public decimal Expense { get; set; }
  public int RevenueCount { get; set; }
  public decimal Revenue { get; set; }
  public int TransactionNumberTotal { get; set; }
  public decimal Balance { get; set; }
}
