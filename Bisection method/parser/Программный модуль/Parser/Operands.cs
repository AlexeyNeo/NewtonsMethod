using System.Collections.Generic;
using System.Linq;

namespace parserDecimal.Parser
{
    class Operands
    {
        public List<string> returnSplitExpression(string expr)
        {
            char[] symbols = expr.ToArray();


            List<string> operands = new List<string>();
            string current = "";

            foreach (char ch in symbols)
            {
                switch (ch)
                {
                    case '(':
                    case ')':
                    case '+':
                    case '-':
                    case '*':
                    case '^':
                    case '/':
                        if (current != "")
                        {
                            operands.Add(current);
                        }
                        current = "";

                        operands.Add(ch.ToString());
                        break;
                    default:
                        current += ch;
                        break;


                }

            }

            if (current != "")
            {
                operands.Add(current);
            }

            List<string> expression = new List<string>();

            foreach (string s in operands)
            {
                expression.Add(s);
            }

            for (int i = 0; i < expression.Count; i++)
            {
                if (i == 0)
                {
                    switch (expression[i])
                    {
                        case "-":
                            expression.Insert(0, "0");
                            break;
                        case "+":
                            expression.RemoveAt(i);
                            break;
                    }
                }
                if (expression[i] == "(")
                {
                    switch (expression[i + 1])
                    {
                        case "-":
                            expression.Insert(i + 1, "0");
                            break;
                        case "+":
                            expression.RemoveAt(i);
                            break;
                    }
                }
            }

            return expression;

        }
    }
}
