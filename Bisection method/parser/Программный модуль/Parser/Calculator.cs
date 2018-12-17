using System.Collections.Generic;
using Extreme.Mathematics;

namespace parserDecimal.Parser
{
    class Calculator
    {
        List<string> standartOperators = new List<string> {
                "+", "-", "*", "/", "^", "%",
                "sqrt", "sin", "cos", "tan",
                "atan", "acos", "asin", "acotan",
                "exp", "ln", "log",
                "sinh", "cosh", "tanh", "abs",
                "ceil", "floor", "fac", "sfac", "round", "fpart"
            };

        public decimal calculate(Queue<string> polska, decimal x)
        {
            decimal answer = 0;

            Stack<decimal> opers = new Stack<decimal>();

            foreach (string s in polska)
            {
                if (standartOperators.Contains(s))
                {
                    decimal a;
                    decimal b;

                    switch (s)
                    {
                        case "+":
                            opers.Push(opers.Pop() + opers.Pop());
                            break;
                        case "-":
                            b = opers.Pop();
                            a = opers.Pop();
                            opers.Push(a - b);
                            break;
                        case "*":
                            opers.Push(opers.Pop() * opers.Pop());
                            break;
                        case "/":
                            b = opers.Pop();
                            a = opers.Pop();
                            opers.Push(a / b);
                            break;
                        case "^":
                            b = opers.Pop();
                            a = opers.Pop();
                            opers.Push(DecimalMath.Pow(a, b));
                            break;
                        case "%":
                            b = opers.Pop();
                            a = opers.Pop();
                            opers.Push(a % b);
                            break;
                        case "sqrt":
                            opers.Push(DecimalMath.Sqrt(opers.Pop()));
                            break;
                        case "sin":
                            opers.Push(DecimalMath.Sin(opers.Pop()));
                            break;
                        case "cos":
                            opers.Push(DecimalMath.Cos(opers.Pop()));
                            break;
                        case "tan":
                            opers.Push(DecimalMath.Tan(opers.Pop()));
                            break;
                        case "atan":
                            opers.Push(DecimalMath.Atan(opers.Pop()));
                            break;
                        case "acos":
                            opers.Push(DecimalMath.Acos(opers.Pop()));
                            break;
                        case "asin":
                            opers.Push(DecimalMath.Asin(opers.Pop()));
                            break;
                        case "acotan":
                            opers.Push(DecimalMath.Atan(1 / opers.Pop()));
                            break;
                        case "exp":
                            opers.Push(DecimalMath.Exp(opers.Pop()));
                            break;
                        case "ln":
                            opers.Push(DecimalMath.Log(opers.Pop()));
                            break;
                        case "log":
                            opers.Push(DecimalMath.Log10(opers.Pop()));
                            break;
                        case "sinh":
                            opers.Push(DecimalMath.Sinh(opers.Pop()));
                            break;
                        case "cosh":
                            opers.Push(DecimalMath.Cosh(opers.Pop()));
                            break;
                        case "tanh":
                            opers.Push(DecimalMath.Tanh(opers.Pop()));
                            break;
                        case "abs":
                            opers.Push(DecimalMath.Abs(opers.Pop()));
                            break;
                        case "ceil":
                            opers.Push(DecimalMath.Ceiling(opers.Pop()));
                            break;
                        case "floor":
                            opers.Push(DecimalMath.Floor(opers.Pop()));
                            break;
                        case "fac":
                            opers.Push(factorial(opers.Pop()));
                            break;
                        case "sfac":
                            opers.Push(semifactorial(opers.Pop()));
                            break;
                        case "round":
                            opers.Push(DecimalMath.Round(opers.Pop()));
                            break;
                        case "fpart":
                            a = opers.Pop();
                            opers.Push(a - DecimalMath.Truncate(a));
                            break;
                    }
                }
                else if (s == "x")
                {
                    opers.Push(x);
                }
                else
                {
                    opers.Push(decimal.Parse(s));
                }
            }

            answer = opers.Pop();
            return answer;
        }

        private decimal semifactorial(decimal v)
        {
            long f = 1;
            long con = (long)v;

            if (con % 2 == 0)
            {
                for (long i = 2; i <= con; i += 2)
                {
                    f *= i;
                }
            }
            else
            {
                for (long i = 1; i <= con; i += 2)
                {
                    f *= i;
                }
            }
            return (decimal)f;
        }

        private decimal factorial(decimal v)
        {
            long f = 1;
            long con = (long)v;

            for (long i = 1; i <= con; i++)
            {
                f *= i;
            }
            return (decimal)f;
        }
    }
}
