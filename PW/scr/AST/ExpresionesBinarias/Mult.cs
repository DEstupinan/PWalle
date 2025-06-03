public class Mult : ExpresionBinaria
{
    public override ExpressionType Type { get; set; }
    public override object Value { get; set; }

    public Mult(CodeLocation location) : base(location) { }
    public override void Calculate()
    {
        Right.Calculate();
        Left.Calculate();
        Value =(double) Right.Value * (double)Left.Value;
    }


}