using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using UnityEngine;
using UnityEngine.UI;

public class DeckCreate : MonoBehaviour
{
    [SerializeField] GameObject FightPad;
    [SerializeField] InputField input;

    [SerializeField] string inputText;
    
    [SerializeField] Text helpText;
    [SerializeField] string text;
    [SerializeField] Color textColor;

    [SerializeField] Transform scrollView;
    [SerializeField] GameObject item;

    private Queue<Button> activeCards = new();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        helpText.text = text;
        helpText.color = textColor;
        helpText.enabled = true;
    }

    public class Card
    {
        public string Name;
        public List<int> Subsequence;
        public bool IsActive;

        public Card(string name, List<int> subsequence)
        {
            Name = name;
            Subsequence = subsequence;
        }
    }

    public static List<Card> cards = new();

    void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.E))
        {
            Pad.pad.enabled = true;
            FightPad.SetActive(true);
            input.text = inputText;
            helpText.enabled = false;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        helpText.enabled = false;
    }

    public void CreateCard()
    {
        var options = ScriptOptions.Default
            .AddImports("System", "System.IO", "System.Collections.Generic",
                "System.Console", "System.Diagnostics");
        try
        {
            var code = @"
                public static class MyClass
                {
                     public static List<int> methods = new();

                        public static class Magic
                        {
                            public static void Fireball()
                            {
                               MyClass.methods.Add(10);
                            }
                        }
                        
                        public static class Physics
                        {
                             public static void KnifeAttack()
                              {
                                  MyClass.methods.Add(1);
                               }
                        } 
                        public static class Heal
                        {
                             public static void PutBandage()
                              {
                                  MyClass.methods.Add(2);
                               }
                        } 
public static class Shields
{
     public static void PutOnShield()
      {
          MyClass.methods.Add(3);
       }
}
public static class Weakeness
{
     public static void WeakenBooze()
      {
          MyClass.methods.Add(4);
       }
} 
                        
                    " +
                    input.text +
                    @"
                }
                MyClass."+ GetMethodName(input.text) + "();"+
                @"
                return MyClass.methods;
                ";


            var result = CSharpScript.EvaluateAsync<List<int>>(code, options);
            result.Wait();

            var card = new Card(GetMethodName(input.text), result.Result);
            cards.Add(card);
            
            var itemGo = Instantiate(item, scrollView, true);
            itemGo.GetComponentInChildren<Text>().text = card.Name;
            var a = itemGo.GetComponent<Button>();

            if(cards.Count <= 3)
            {
                card.IsActive = true;
                a.interactable  = false;
                activeCards.Enqueue(a);
            }
        }

        catch (Exception e)
        {
            Debug.Log("WARNING : " + e.Message);
        }
    }

    public void doada(Button button)
    {
        var a = activeCards.Dequeue();
        a.interactable = true;
        
        activeCards.Enqueue(button);
    }

    public static string GetMethodName(string methodString)
    {
        var method = methodString.Substring(0, methodString.IndexOf('('));
        var methodName = method.Substring(method.LastIndexOf('.') + 1);
        var voidIndex = method.IndexOf("void");
        methodName = method.Substring(voidIndex + 4, methodName.Length - voidIndex - 4).Replace(" ", "");
        return methodName;
    }
}