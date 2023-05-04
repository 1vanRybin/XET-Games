using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using UnityEngine;

public class CodeCompiler : MonoBehaviour
{
    [SerializeField] string lacodeToCompile;

    private void Start()
    {
        execute(lacodeToCompile);
    }
    public void execute(string codeToEvaluate)
    {
        try
        {
            var result = CSharpScript.EvaluateAsync(codeToEvaluate).GetAwaiter().GetResult();
            Debug.Log("Result => " + codeToEvaluate);
            Debug.Log(result);
        }
        catch (Exception e)
        {
            Debug.LogWarning("WARNING : " + e);
        }
    }
}
