using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using Bisection_method.Model;
using parserDecimal.Parser;

namespace Newtons_method
{
    public class NewtonsMethod
    {
         public event Action<int> ProgressBarIncrement; 
        readonly Computer _computer = new Computer();
        readonly Derivative _derivative = new Derivative();
        private decimal FunctionCalculate(decimal x1, string func)
        {
            return _computer.Compute(func, x1);
        }


        private int sign(decimal x)
        {
            if (x > 0) { return 1; }
            else if (x == 0) { return 0; }
            else if (x < 0) { return -1; }
            else return 5;
        }


        private string GetDerivativeString(string function)
        {
            Derivative derivative = new Derivative();
            return derivative.ReturnDerivative(function);
        }

        public NewtonsViewModel Calculate(NewtonsModel model)
        {
            decimal DDFX0, DFX0, DP = 0, DP0 = 0, x0 = 0, x1 = 0, fx1 = 0, DFX1 = 0, DDFX1 = 0, RelError = 0;
            int cond = 0;
            string derivative1 = GetDerivativeString(model.Func);
            string derivative2 = GetDerivativeString(derivative1);
            decimal fx = FunctionCalculate(model.PointX, model.Func);
            x0 = model.PointX;

            DFX0 = FunctionCalculate(model.PointX, derivative1);
            DDFX0 = FunctionCalculate(model.PointX, derivative2);
            int k = 0;

            do
            {
                if (cond == 2)
                    break;

                k++;
                ProgressBarIncrement?.Invoke(1);
                DDFX0 = FunctionCalculate(x0, derivative2);

                if (Math.Abs(DDFX0) <= model.Epsilon)
                    cond = 1;
                else
                    DP = DFX0 / DDFX0;
                if (k == 1)
                    DP0 = DP;

                if (Math.Sign(DP0) == Math.Sign(DP))
                    x1 = x0 - DP;
                else
                    x1 = x0 - DP / model.R;

                DP0 = DP;

                fx1 = FunctionCalculate(x1, model.Func);
                DFX1 = FunctionCalculate(x1, derivative1);
                RelError = 2 * Math.Abs(DP) / Math.Abs(x1) + model.Epsilon;

                if (RelError < model.Delta && cond != 1)
                    cond = 2;
                x0 = x1;
                DFX0 = DFX1;
            } while (k <= model.IterationMax);


            if (k < model.IterationMax)
            {
                ProgressBarIncrement?.Invoke(model.IterationMax - k + 1);
            }

            return new NewtonsViewModel
            {
                RelError = RelError,
                Fx = fx,
                Iteration = k,
                X = x1,
                Dfx1 = DFX1,
                Cond = cond,
            };
        }

    }
}
