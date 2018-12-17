using System.Collections.Generic;

namespace parserDecimal.Parser
{
    class Simplify
    {
        List<string> standartOperators = new List<string> {
                "+", "-", "*", "/", "^", "%",
                "sqrt", "sin", "cos", "tan",
                "atan", "acos", "asin", "acotan",
                "exp", "ln", "log",
                "sinh", "cosh", "tanh", "abs",
                "ceil", "floor", "fac", "sfac", "round", "fpart"
            };

        List<string> prior2 = new List<string>
            {
                "^"
            };

        List<string> prior1 = new List<string>
            {
                 "sqrt", "sin", "cos", "tan",
                "atan", "acos", "asin", "acotan",
                "exp", "ln", "log",
                "sinh", "cosh", "tanh", "abs",
                "ceil", "floor", "fac", "sfac", "round", "fpart"
            };

        List<string> prior3 = new List<string>
            {
                "*", "/", "%"
            };


        List<string> prior4 = new List<string>
            {
                "+", "-"
            };

        Stack<string> polish = new Stack<string>();

        public string NoBraces(Queue<string> polska)
        {
            polish = new Stack<string>();

            foreach (string s in polska)
            {
                polish.Push(s);
            }

            string lastOperator = polish.Pop();
            if (standartOperators.Contains(lastOperator))
            {
                string answer = DeleteBraces(polish, lastOperator);
                while (answer.Contains("++") || answer.Contains("+-") || answer.Contains("-+") || answer.Contains("--"))
                {
                    answer = answer.Replace("++", "+");
                    answer = answer.Replace("+-", "-");
                    answer = answer.Replace("-+", "-");
                    answer = answer.Replace("--", "+");
                }
                return answer;
            }
            else
            {
                return lastOperator;
            }
        }

        private string DeleteBraces(Stack<string> polish, string previousOperator)
        {
            string a;
            string b;

            if (prior1.Contains(previousOperator))
            {
                a = polish.Pop();
                if (standartOperators.Contains(a))
                {
                    a = DeleteBraces(polish, a);
                }
                return (previousOperator + "(" + a + ")");
            }
            else if (prior2.Contains(previousOperator))
            {
                b = polish.Pop();
                if (standartOperators.Contains(b))
                {
                    if (prior1.Contains(b))
                    {
                        b = DeleteBraces(polish, b);
                    }
                    else
                    {
                        b = "(" + DeleteBraces(polish, b) + ")";
                    }
                }

                a = polish.Pop();
                if (standartOperators.Contains(a))
                {
                    if (prior1.Contains(a))
                    {
                        a = DeleteBraces(polish, a);
                    }
                    else
                    {
                        a = "(" + DeleteBraces(polish, a) + ")";
                    }
                }

                if (b == "(1)" || b == "1")
                {
                    return a;
                }
                else
                {
                    return a + previousOperator + b;
                }

            }
            else if (prior3.Contains(previousOperator))
            {
                b = polish.Pop();
                if (standartOperators.Contains(b))
                {
                    if (prior4.Contains(b))
                    {
                        b = "(" + DeleteBraces(polish, b) + ")";
                    }
                    else
                    {
                        b = DeleteBraces(polish, b);
                    }
                }

                a = polish.Pop();
                if (standartOperators.Contains(a))
                {
                    if (prior4.Contains(a))
                    {
                        a = "(" + DeleteBraces(polish, a) + ")";
                    }
                    else
                    {
                        a = DeleteBraces(polish, a);
                    }
                }

                if (a == "0" || b == "0")
                {
                    if (previousOperator == "*")
                    {
                        return "0";
                    }
                    else
                    {
                        if (a == "0" && b != "0")
                        {
                            return "0";
                        }
                        else
                        {
                            return "error!";
                        }
                    }
                }
                else
                {
                    if (previousOperator == "*")
                    {
                        if (a == "1")
                        {
                            return b;
                        }

                        if (b == "1")
                        {
                            return a;
                        }
                    }
                    else
                    {
                        if (b == "1")
                        {
                            return a;
                        }
                    }
                    return a + previousOperator + b;
                }
            }
            else
            {
                //надо добавить упрощение выражений
                b = polish.Pop();
                if (standartOperators.Contains(b))
                {
                    b = DeleteBraces(polish, b);
                }

                a = polish.Pop();
                if (standartOperators.Contains(a))
                {
                    a = DeleteBraces(polish, a);
                }

                if (a == "0" || b == "0")
                {
                    if (previousOperator == "+")
                    {
                        if (a == "0")
                        {
                            return b;
                        }
                        else
                        {
                            return a;
                        }
                    }
                    else
                    {
                        if (a == "0" && b == "0")
                        {
                            return "0";
                        }
                        else
                        {
                            if (a == "0")
                            {
                                return previousOperator + b;
                            }
                            else
                            {
                                return a;
                            }
                        }
                    }
                }

                decimal checkDecimal;
                if (decimal.TryParse(a, out checkDecimal) && decimal.TryParse(b, out checkDecimal))
                {
                    if (previousOperator == "-")
                    {
                        return (decimal.Parse(a) - decimal.Parse(b)).ToString();
                    }
                    else
                    {
                        return (decimal.Parse(a) + decimal.Parse(b)).ToString();
                    }
                }
                else
                {
                    return a + previousOperator + b;
                }
            }
        }
    }
}
