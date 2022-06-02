using CVPZ.Core;
using System;

namespace CVPZ.Application.Test.Core;

public static class ErrorExtensions
{
    public static void Assert(this Error error)
    {
        throw new Exception($"{error.Code}: /n/r{error.Message}");
    }

}
