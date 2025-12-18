using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyFinancialControl.Communication.ViewObjects.API;

public class APIResponse
{
    public bool Success { get; set; }
    public int Number { get; set; }
    public object Object { get; set; }
    public string Message { get; set; }

    public APIResponse()
    {
    }
    public APIResponse(string message, int number, object @object, bool success)
    {
        Message = message;
        Number = number;
        Object = @object;
        Success = success;
    }

    public static APIResponse Ok(string message = null, object obj = null, int number = 0)
    {
        return new APIResponse(message, number, obj, true);
    }

    public static APIResponse Fail(string message = null, object obj = null, int number = 0)
    {
        return new APIResponse(message, number, obj, false);
    }
}