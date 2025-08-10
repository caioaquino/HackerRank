using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Result
{

    /*
     * Complete the 'isBalanced' function below.
     *
     * The function is expected to return a STRING.
     * The function accepts STRING s as parameter.
     */

    public static string isBalanced(string s)
    {
            Stack<char> stack = new Stack<char>();
            char[] chars = s.ToArray();
            char[] openingBrackets = ['(','{','['];
            char[] closingBrackets = [')','}',']'];
            
            foreach(char item in chars){
                Console.WriteLine(item);
                if(openingBrackets.Contains(item)){
                    stack.Push(item);
                }
                else if(closingBrackets.Contains(item)){
                    char? peekChar = stack.Count > 0 ? stack.Peek() : null;
                    if((peekChar == '[' && item == ']') || (peekChar == '(' && item == ')') || (peekChar == '{' && item == '}')) 
                        stack.Pop();
                    else {stack.Push(item);
                            break;}
                }
            }

            if(stack.Count == 0)
                return "YES";
            return "NO";
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int t = Convert.ToInt32(Console.ReadLine().Trim());

        for (int tItr = 0; tItr < t; tItr++)
        {
            string s = Console.ReadLine();

            string result = Result.isBalanced(s);

            textWriter.WriteLine(result);
        }

        textWriter.Flush();
        textWriter.Close();
    }
}
