using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyFinancialControl.Helpers;

public static class StaticMethods
{
    public static string ExtractResultMessage(Result result)
    {
        try
        {
            if (result.IsSuccess)
                if (result.Successes.FirstOrDefault() != null)
                    return result.Successes.FirstOrDefault()?.Message ?? ConstantsMessagesGeneric.ErrorGeneric;
                else
                    return ConstantsMessagesGeneric.ErrorGeneric;
            else
                return result.Errors.FirstOrDefault()?.Message ?? ConstantsMessagesGeneric.ErrorGeneric;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static string ExtractResultMessage<T>(Result<T> result)
    {
        try
        {
            if (result.IsSuccess)
                if (result.Successes.FirstOrDefault() != null)
                    return result.Successes.FirstOrDefault()?.Message ?? "Erro";
                else
                    return "";
            else
                return result.Errors.FirstOrDefault()?.Message ?? "Erro";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}
