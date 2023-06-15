using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Mono.Cecil.Cil;
using UnityEditor;
using UnityEngine;

public class CodeCompiler : MonoBehaviour
{
    public static string Execute(string codeToEvaluate)
    {
        var options = ScriptOptions.Default
            .AddImports("System", "System.IO", "System.Collections.Generic",
                "System.Console", "System.Diagnostics");
        try
        {
            var code = @"
                using System;
                using System.Collections.Generic;

                public static class MyClass
                {
                        public static List<int> MyMethod()
                    {
                        return new List<int>() {
                            MiddleOf(1, 30, 2),
                            MiddleOf(2,3,4),
                            MiddleOf(6, 7, 5)};
                    }

                    " +
                    codeToEvaluate +
                    @"
                }

                return MyClass.MyMethod();
                ";


            var result = CSharpScript.EvaluateAsync<List<int>>(code, options);
            result.Wait();
            var answer=string.Join(" ", result.Result);

            return answer;
        }

        catch (Exception e)
        {
            return "WARNING : " + e.Message;
        }
    }
}