using System.Collections.Generic;

namespace parserDecimal.Parser
{
    class Polish
    {
        Operands operands = new Operands();

        List<string> standartOperators = new List<string> {
                "+", "-", "*", "/", "^", "%",
                "sqrt", "sin", "cos", "tan",
                "atan", "acos", "asin", "acotan",
                "exp", "ln", "log",
                "sinh", "cosh", "tanh", "abs",
                "ceil", "floor", "fac", "sfac", "round", "fpart"
            };

        List<string> braces = new List<string>
            {
                "(", ")"
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

        public Queue<string> returnPolish(List<string> expression)
        {
            Stack<string> operators = new Stack<string>();
            Queue<string> polska = new Queue<string>();


            foreach (string s in expression)
            {
                if (braces.Contains(s))
                {
                    if (s == "(")
                    {
                        operators.Push(s);
                    }
                    else
                    {
                        while (operators.Peek() != "(")
                        {
                            polska.Enqueue(operators.Peek());
                            operators.Pop();
                        }
                        if (operators.Peek() == "(")
                        {
                            operators.Pop();
                        }
                    }
                }
                else if (standartOperators.Contains(s))
                {
                    if (prior1.Contains(s))
                    {
                        while (operators.Count != 0 && prior1.Contains(operators.Peek()))
                        {
                            polska.Enqueue(operators.Peek());
                            operators.Pop();
                        }
                        operators.Push(s);
                    }
                    else if (prior2.Contains(s))
                    {
                        while (operators.Count != 0 && (prior1.Contains(operators.Peek()) || prior2.Contains(operators.Peek())))
                        {
                            polska.Enqueue(operators.Peek());
                            operators.Pop();
                        }
                        operators.Push(s);
                    }
                    else if (prior3.Contains(s))
                    {
                        while (operators.Count != 0 && (prior1.Contains(operators.Peek()) || prior2.Contains(operators.Peek()) || prior2.Contains(operators.Peek())))
                        {
                            polska.Enqueue(operators.Peek());
                            operators.Pop();
                        }
                        operators.Push(s);
                    }
                    else
                    {
                        while (operators.Count != 0 &&
                            (prior1.Contains(operators.Peek())
                        || prior2.Contains(operators.Peek())
                        || prior3.Contains(operators.Peek())
                        || prior4.Contains(operators.Peek())))
                        {
                            polska.Enqueue(operators.Peek());
                            operators.Pop();
                        }
                        operators.Push(s);
                    }
                }
                else
                {
                    polska.Enqueue(s);
                }
            }


            while (operators.Count != 0)
            {
                polska.Enqueue(operators.Peek());
                operators.Pop();
            }

            return checking(polska);
        }

        private Queue<string> checking(Queue<string> polska)
        {
            Stack<string> opers = new Stack<string>();

            foreach (string s in polska)
            {
                if (standartOperators.Contains(s))
                {
                    if (prior1.Contains(s))
                    {
                        opers.Push(opers.Pop() + s);
                    }
                    else
                    {
                        opers.Push(opers.Pop() + opers.Pop() + s);
                    }
                }
                else
                {
                    opers.Push(s);
                }
            }

            if (opers.Count != 1)
            {
                return null;
            }
            else
            {
                return polska;
            }
        }
    }
}
