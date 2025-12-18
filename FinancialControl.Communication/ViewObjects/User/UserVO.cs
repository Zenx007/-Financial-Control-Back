using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyFinancialControl.Communication.ViewObjects.User;

public class UserVO
{
    public int Id { get; set; }
    [Required (ErrorMessage = "Erro, o nome do usuário é obrigatório")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Erro, a idade do usuário é obrigatória")]
    public int Age { get; set; }
}
