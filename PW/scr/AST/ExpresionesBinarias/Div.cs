public class Div : ExpresionBinaria
{
    public override ExpressionType Type { get; set; }
    public override object Value { get; set; }

    public Div(CodeLocation location) : base(location) { }
    public override void Calculate()
    {
        Right.Calculate();
        Left.Calculate();
        Value = (double)Left.Value / (double)Right.Value;
    }


}