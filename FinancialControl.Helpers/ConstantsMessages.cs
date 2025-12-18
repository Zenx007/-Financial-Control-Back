using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyFinancialControl.Helpers;

public static class ConstantsMessagesGeneric
{
    //Error
    public static string ErrorGeneric = "Erro ao realizar esta ação";
}

public static class ConstantsMessagesUser
{
    //Error
    public static string ErrorNotFound = "Usuário não encontrado";
    public static string ErrorSave = "Erro ao salvar usuário";
    public static string ErrorUpdate = "Erro ao atualizar usuário";
    public static string ErrorDelete = "Erro ao deletar usuário";
    public static string ErrorGetAll = "Erro ao buscar usuários";
    public static string Prepare = "Erro ao preparar dados do usuário";

    //Success
    public static string SuccessSaved = "Usuário salvo com sucesso";
    public static string SuccessDelete = "Usuário deletado com sucesso";
}

public static class ConstantsMessagesTransaction
{
    //Error
    public static string ErrorNotFound = "Transação não encontrada";
    public static string ErrorSave = "Erro ao salvar transação";
    public static string ErrorUpdate = "Erro ao atualizar transação";
    public static string ErrorDelete = "Erro ao deletar transação";
    public static string ErrorGetAll = "Erro ao buscar transações";
    public static string ErrorPrepare = "Erro ao preparar dados da transação";
    public static string ErrorAgeRestriction = "Menores de 18 anos só podem registrar despesas";
    public static string ErrorCategoryMismatch = "O tipo da transação não condiz com a finalidade da categoria";
    public static string ErrorGetTotals = "Erro ao carregar o total das transações";

    //Success
    public static string SuccessSaved = "Transação salva com sucesso";
    public static string SuccessDelete = "Transação deletada com sucesso";
    public static string SuccessUpdate = "Transação atualizada com sucesso";
}

public static class ConstantsMessagesCategory
{
    //Error
    public static string ErrorNotFound = "Categoria não encontrada";
    public static string ErrorSave = "Erro ao salvar categoria";
    public static string ErrorUpdate = "Erro ao atualizar a categoria";
    public static string ErrorPrepare = "Erro ao preparar os dados da categoria";
    public static string ErrorDelete = "Erro ao deletar categoria";
    public static string ErrorGetAll = "Erro ao buscar categorias";
    public static string ErrorGetAllDetails = "Erro ao buscar todos os detalhes por categoria";

    //Success
    public static string SuccessSaved = "Categoria salva com sucesso";
    public static string SuccessDelete = "Categoria deletada com sucesso";
    public static string SuccessUpdate = "Categoria atualizada com sucesso";
}