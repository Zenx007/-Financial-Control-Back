using FamilyFinancialControl.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyFinancialControl.Core.Entities;

public class Transaction
{
    [Column("id")]
    public int Id { get; set; }
    [Column("description")]
    public string Description { get; set; }
    [Column("value")]
    public decimal Value { get; set; }
    [Column("type_transaction")]
    public TypeTransaction TypeTransaction { get; set; }
    [Column("category_id")]
    public int CategoryId { get; set; }
    [Column("user_id")]
    public int UserId { get; set; }

    public virtual Category Category { get; set; }
    public virtual User User { get; set; }
}
