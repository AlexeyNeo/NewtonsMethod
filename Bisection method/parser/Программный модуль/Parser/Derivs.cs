using System.Collections.Generic;
using System.Linq;

namespace parserDecimal.Parser
{
    class Derivs
    {
        List<string> standartOperators = new List<string> {
                "+", "-", "*", "/", "^", "%",
                "sqrt", "sin", "cos", "tan",
                "atan", "acos", "asin", "acotan",
                "exp", "ln", "log",
                "sinh", "cosh", "tanh", "abs",
                "ceil", "floor", "fac", "sfac", "round", "fpart"
            };

        List<string> prior1 = new List<string>
            {
                 "sqrt", "sin", "cos", "tan",
                "atan", "acos", "asin", "acotan",
                "exp", "ln", "log",
                "sinh", "cosh", "tanh", "abs",
                "ceil", "floor", "fac", "sfac", "round", "fpart"
            };

        Operands operands = new Operands();
        Polish polish = new Polish();

        public string Derivative(List<string> polska)
        {
            Stack<string> opers = new Stack<string>();
            List<string> deriv = new List<string>();
            string answer = "";
            string a;
            string b;

            if (polska.Count == 1)
            {
                decimal d;
                if (decimal.TryParse(polska.Last(), out d))
                {
                    answer = "0";
                }

                if (polska.Last() == "x")
                {
                    answer = "1";
                }
            }
            else
            {
                switch (polska.Last())
                {
                    case "+":
                        polska.RemoveAt(polska.Count - 1);
                        opers = GetStack(polska);
                        b = opers.Pop();
                        a = opers.Pop();
                        answer = "([" + a + "]" + "+" + "[" + b + "])";
                        break;
                    case "-":
                        polska.RemoveAt(polska.Count - 1);
                        opers = GetStack(polska);
                        b = opers.Pop();
                        a = opers.Pop();
                        answer = "([" + a + "]" + "-" + "[" + b + "])";
                        break;
                    case "*":
                        polska.RemoveAt(polska.Count - 1);
                        opers = GetStack(polska);
                        b = opers.Pop();
                        a = opers.Pop();
                        answer = "([" + a + "]*" + b + "+" + "[" + b + "]*" + a + ")";
                        break;
                    case "/":
                        polska.RemoveAt(polska.Count - 1);
                        opers = GetStack(polska);
                        b = opers.Pop();
                        a = opers.Pop();
                        answer = "(([" + a + "]*" + b + "-" + "[" + b + "]*" + a + ")/(" + b + ")^2)";
                        break;
                    case "^":
                        polska.RemoveAt(polska.Count - 1);
                        opers = GetStack(polska);
                        b = opers.Pop();
                        a = opers.Pop();
                        //a^b = b * a^(b-1) * a' + a^b * ln(a) * b'
                        answer = "(" + b + "*" + a + "^(" + b + "-1)*[" + a + "]" + "+" + a + "^" + b + "*ln(" + a + ")*[" + b + "])";
                        break;
                    case "sqrt":
                        polska.RemoveAt(polska.Count - 1);
                        opers = GetStack(polska);
                        a = opers.Pop();
                        answer = "((" + a + ")^(-0.5)*[" + a + "]/2)";
                        break;
                    case "sin":
                        polska.RemoveAt(polska.Count - 1);
                        opers = GetStack(polska);
                        a = opers.Pop();
                        answer = "(cos(" + a + ")*[" + a + "])";
                        break;
                    case "cos":
                        polska.RemoveAt(polska.Count - 1);
                        opers = GetStack(polska);
                        a = opers.Pop();
                        answer = "((-sin(" + a + "))*[" + a + "])";
                        break;
                    case "tan":
                        polska.RemoveAt(polska.Count - 1);
                        opers = GetStack(polska);
                        a = opers.Pop();
                        answer = "([" + a + "]/(cos(" + a + "))^2)";
                        break;
                    case "atan":
                        polska.RemoveAt(polska.Count - 1);
                        opers = GetStack(polska);
                        a = opers.Pop();
                        answer = "([" + a + "]/(1+" + a + "^2))";
                        break;
                    case "acos":
                        polska.RemoveAt(polska.Count - 1);
                        opers = GetStack(polska);
                        a = opers.Pop();
                        answer = "((-[" + a + "])/(1-" + a + "^2)^(0.5))";
                        break;
                    case "asin":
                        polska.RemoveAt(polska.Count - 1);
                        opers = GetStack(polska);
                        a = opers.Pop();
                        answer = "([" + a + "]/(1-" + a + "^2)^(0.5))";
                        break;
                    case "acotan":
                        polska.RemoveAt(polska.Count - 1);
                        opers = GetStack(polska);
                        a = opers.Pop();
                        answer = "((-[" + a + "])/(1+" + a + "^2))";
                        break;
                    case "exp":
                        polska.RemoveAt(polska.Count - 1);
                        opers = GetStack(polska);
                        a = opers.Pop();
                        answer = "(exp(" + a + ")*[" + a + "])";
                        break;
                    case "ln":
                        polska.RemoveAt(polska.Count - 1);
                        opers = GetStack(polska);
                        a = opers.Pop();
                        answer = "([" + a + "]/(" + a + "))";
                        break;
                    case "log":
                        polska.RemoveAt(polska.Count - 1);
                        opers = GetStack(polska);
                        a = opers.Pop();
                        answer = "([" + a + "]/(x*ln(10)))";
                        break;
                    case "sinh":
                        polska.RemoveAt(polska.Count - 1);
                        opers = GetStack(polska);
                        a = opers.Pop();
                        answer = "([" + a + "]*" + "cosh(" + a + "))";
                        break;
                    case "cosh":
                        polska.RemoveAt(polska.Count - 1);
                        opers = GetStack(polska);
                        a = opers.Pop();
                        answer = "([" + a + "]*" + "sinh(" + a + "))";
                        break;
                    case "tanh":
                        polska.RemoveAt(polska.Count - 1);
                        opers = GetStack(polska);
                        a = opers.Pop();
                        answer = "([" + a + "]/" + "cosh(" + a + ")^2)";
                        break;
                }

            }

            return GetSimpleDerivatives(answer);
        }

        private string GetSimpleDerivatives(string answer)
        {
            answer = answer.Replace("[x]", "1");
            answer = answer.Replace("[exp(x)]", "exp(x)");
            answer = answer.Replace("[ln(x)]", "1/x");
            answer = answer.Replace("[sin(x)]", "cos(x)");
            answer = answer.Replace("[cos(x)]", "(-sin(x))");
            answer = answer.Replace("[sqrt(x)]", "1/(2*x^0.5)");
            answer = answer.Replace("[tan(x)]", "1/(cos(x)^2)");
            answer = answer.Replace("[cotan(x)]", "(-1/(sin(x)^2))");
            answer = answer.Replace("[asin(x)]", "1/(1-x^2)^0.5");
            answer = answer.Replace("[acos(x)]", "(-1/(1-x^2)^0.5)");
            answer = answer.Replace("[atan(x)]", "1/(1+x^2)");
            answer = answer.Replace("[acotan(x)]", "(-1/(1+x^2))");
            answer = answer.Replace("[sinh(x)]", "cosh(x)");
            answer = answer.Replace("[cosh(x)]", "sinh(x)");
            answer = answer.Replace("[tanh(x)]", "1/cosh(x)^2");
            return answer;
        }

        internal string GetDerivative(List<string> list)
        {
            string answer = Derivative(list);
            string beginning = "";
            string middle = "";
            string end = "";

            while (answer.Contains('['))
            {
                char[] ans = answer.ToArray();

                bool flagBeg = true;
                bool flagMid = false;

                foreach (char a in ans)
                {
                    if (a == '[' && flagBeg == true)
                    {
                        flagBeg = false;
                        flagMid = true;
                        continue;
                    }

                    if (a == ']' && flagMid == true)
                    {
                        flagMid = false;
                        continue;
                    }

                    if (flagBeg)
                    {
                        beginning += a.ToString();
                    }
                    else if (flagMid)
                    {
                        middle += a.ToString();
                    }
                    else
                    {
                        end += a.ToString();
                    }
                }

                List<string> splitExpression = operands.returnSplitExpression(middle);
                Queue<string> reversePolish = polish.returnPolish(splitExpression);
                middle = Derivative(reversePolish.ToList());
                answer = beginning + middle + end;
                beginning = "";
                middle = "";
                end = "";
            }

            return answer;
        }


        private Stack<string> GetStack(List<string> polska)
        {
            Stack<string> opers = new Stack<string>();
            string a;
            string b;

            foreach (string s in polska)
            {
                if (standartOperators.Contains(s))
                {
                    if (prior1.Contains(s))
                    {
                        opers.Push(s + "(" + opers.Pop() + ")");
                    }
                    else
                    {
                        switch (s)
                        {
                            case "+":
                                b = opers.Pop();
                                a = opers.Pop();
                                opers.Push(a + "+" + b);
                                break;
                            case "-":
                                b = opers.Pop();
                                a = opers.Pop();
                                opers.Push(a + "-" + b);
                                break;
                            case "*":
                                b = opers.Pop();
                                a = opers.Pop();
                                opers.Push(a + "*" + b);
                                break;
                            case "/":
                                b = opers.Pop();
                                a = opers.Pop();
                                opers.Push(a + "/" + b);
                                break;
                            case "^":
                                b = opers.Pop();
                                a = opers.Pop();
                                opers.Push(a + "^" + b);
                                break;
                            case "%":
                                b = opers.Pop();
                                a = opers.Pop();
                                opers.Push(a + "%" + b);
                                break;

                        }
                    }
                }
                else
                {
                    opers.Push(s);
                }
            }

            return opers;
        }
    }
}
