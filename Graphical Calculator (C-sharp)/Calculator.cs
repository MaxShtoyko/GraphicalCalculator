using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphical_Calculator__C_sharp_
{
    class Calculator
    {
        static private int GetPriority(char symbol)
        {
            switch (symbol)
            {
                case '(': return 0;
                case ')': return 1;
                case '+': return 2;
                case '-': return 2;
                case '*': return 4;
                case '/': return 4;
                case '^': return 5;
                default: return -1;
            }
        }

        static private string GetExpression(string exp)
        {
            string expression = "";
            Stack<char> stack = new Stack<char>();

            for (int i = 0; i < exp.Length; i++)
            {
                if (exp[i] == ' ' || exp[i] == '=')
                {
                    continue;
                }

                if ("1234567890".IndexOf(exp[i]) != -1)
                {
                    while ("1234567890".IndexOf(exp[i]) != -1)
                    {
                        expression += exp[i];
                        i++;

                        if (i == exp.Length)
                            break;
                    }
                    expression += " ";
                    i--;
                }

                if ("+-^*/()".IndexOf(exp[i]) != -1)
                {
                    if (i != (exp.Length - 1))
                    {
                        if (exp[i] == '-' && ("1234567890".IndexOf(exp[i + 1]) != -1))
                        {
                            if (i == 0)
                            {
                                expression += exp[i];
                                continue;
                            }
                            else if(i>0 && "+-^*/()".IndexOf(exp[i])!=-1)
                            {
                                expression += exp[i];
                                continue;
                            }
                        }
                    }
                    if (exp[i] == '(')
                        stack.Push(exp[i]);

                    else if (exp[i] == ')')
                    {
                        char symbol = stack.Pop();

                        while (symbol != '(')
                        {
                            expression += symbol.ToString() + " ";
                            symbol = stack.Pop();
                        }
                    }

                    else
                    {
                        if (stack.Count > 0)
                        {
                            if (GetPriority(stack.Peek()) >= GetPriority(exp[i]))
                            {
                                expression += stack.Pop().ToString() + " ";
                            }
                        }

                        stack.Push(char.Parse(exp[i].ToString()));
                    }
                }

            }

            while (stack.Count > 0)
            {
                expression += stack.Pop().ToString() + " ";
            }

            return expression;
        }

        static private double GetResult(string exp)
        {
            Stack<double> stack = new Stack<double>();


            for (int i = 0; i < exp.Length; i++)
            {
                if ("1234567890".IndexOf(exp[i]) != -1)
                {
                    string temp = "";

                    while ("1234567890".IndexOf(exp[i]) != -1)
                    {
                        temp += exp[i];
                        i++;
                        if (i == exp.Length)
                            break;
                    }
                    stack.Push(double.Parse(temp));
                    i--;
                }
                else if ("+-*/^".IndexOf(exp[i]) != -1)
                {
                    if(exp[i]=='-' && i!=(exp.Length-1))
                    {
                        string temp = "";
                       
                        if ("1234567890".IndexOf(exp[i+1]) != -1)
                        {
                            temp += exp[i];
                            i++;
                            while ("1234567890".IndexOf(exp[i]) != -1)
                            {
                                temp += exp[i];
                                i++;
                                if (i == exp.Length)
                                    break;
                            }
                            stack.Push(double.Parse(temp));
                            i--;
                            continue;
                        }
                    }
                    double x = stack.Pop();
                    double y = stack.Pop();

                    switch (exp[i])
                    {
                        case '+':
                            {
                                stack.Push(y + x);
                                break;
                            }
                        case '-':
                            {
                                stack.Push(y - x);
                                break;
                            }
                        case '*':
                            {
                                stack.Push(y * x);
                                break;
                            }
                        case '/':
                            {
                                stack.Push(y / x);
                                break;
                            }
                        case '^':
                            {
                                stack.Push(Math.Pow(y, x));
                                break;
                            }
                    }

                }
            }
            return stack.Pop();
        }

        static public double Calc(string exp)
        {
            string expression;
            double result;
            expression = GetExpression(exp);
            result = GetResult(expression);
            return result;
        }
    }
}
