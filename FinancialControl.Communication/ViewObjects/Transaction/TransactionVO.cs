using FamilyFinancialControl.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyFinancialControl.Communication.ViewObjects.Transaction;

    public class TransactionVO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Erro, a descrição da transação é obrigatória")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Erro, o valor da transação é obrigatório")]
        [Range(0.01, 100000000, ErrorMessage = "O valor deve estar entre 0,01 e 100.000.000")]
        public decimal Value { get; set; }
        [Required(ErrorMessage = "Erro, o tipo da transação é obrigatório")]
        public TypeTransaction TypeTransaction { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
    }
