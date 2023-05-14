using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using UnityEngine;

public class CodeCompiler : MonoBehaviour
{
    public static object Execute(string codeToEvaluate)
    {
        try
        {
            var result = CSharpScript.EvaluateAsync(codeToEvaluate).GetAwaiter().GetResult();
            return result;
        }

        catch (Exception e)
        {
            return "WARNING : " + e.Message;
        }
    }
}
