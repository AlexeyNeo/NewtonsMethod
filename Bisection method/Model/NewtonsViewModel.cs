namespace Bisection_method.Model
{
    public class NewtonsViewModel
    {
        public int Iteration { get; set; }
        public decimal RelError { get; set; }
        public string Error { get; set; }
        public decimal Fx { get; set; }
        public decimal X { get; set; }
        public double time { get; set; }
        public decimal Dfx1 { get; set; }
        public  int Cond { get; set; }

    }
}