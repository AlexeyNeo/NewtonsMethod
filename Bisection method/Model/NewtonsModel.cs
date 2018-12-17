namespace Bisection_method.Model
{
    public class NewtonsModel
    {
         public decimal PointX { get; set; }
         public string Func { get; set; }
         public double Tol { get; set; }
        public decimal Epsilon { get; set; }
        public decimal Delta { get; set; }
        public decimal R { get; set; }
         public int IterationMax { get; set; }
       
    }
}