public class Or : ExpresionBinaria
{
    public override ExpressionType Type { get; set; }
    public override object Value { get; set; }

    public Or(CodeLocation location) : base(location) { }
    public override void Calculate()
    {
        Right.Calculate();
        Left.Calculate();
        if ((bool)Left.Value || (bool)Right.Value) Value = (bool)true;
        else Value = (bool)false;
    }


}