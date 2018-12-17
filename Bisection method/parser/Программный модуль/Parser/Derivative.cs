using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parserDecimal.Parser
{
    class Derivative
    {
        Operands operands = new Operands();
        Polish polish = new Polish();
        Calculator calculator = new Calculator();
        Derivs derivative = new Derivs();
        Simplify simplify = new Simplify();

        internal string ReturnDerivative(string function)
        {
            List<string> splitExpression = operands.returnSplitExpression(function.Replace(" ","").ToLower());
            Queue<string> revpExpression = polish.returnPolish(splitExpression);

            string deriv = derivative.GetDerivative(revpExpression.ToList());

            List<string> splitDerivative = operands.returnSplitExpression(deriv);
            Queue<string> revpDerivative = polish.returnPolish(splitDerivative);
            deriv = simplify.NoBraces(revpDerivative);
            return deriv;
        }
    }
}
