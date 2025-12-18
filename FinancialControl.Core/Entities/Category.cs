using FamilyFinancialControl.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyFinancialControl.Core.Entities;

public class Category
{
    [Column("id")]
    public int Id { get; set; }
    [Column("description")]
    public string Description { get; set; }
    [Column("type_category")]
    public TypeCategory TypeCategory { get; set; }

    public List<Transaction> Transactions { get; set; }
}
