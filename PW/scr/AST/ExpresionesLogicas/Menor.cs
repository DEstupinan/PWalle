public class Menor : ExpresionBinaria
{
    public override ExpressionType Type { get; set; }
    public override object Value { get; set; }

    public Menor(CodeLocation location) : base(location) { }
    public override void Calculate()
    {
        Right.Calculate();
        Left.Calculate();
        if ((double)Left.Value < (double)Right.Value) Value = (bool)true;
        else Value = (bool)false;
    }


}