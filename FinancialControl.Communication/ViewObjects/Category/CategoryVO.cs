using FamilyFinancialControl.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyFinancialControl.Communication.ViewObjects.Category;

public class CategoryVO
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Erro, a descrição da categoria é obrigatória")]
    public string Description { get; set; }
    [Required(ErrorMessage = "Erro, o tipo da categoria é obrigatório")]
    public TypeCategory TypeCategory { get; set; }
}
