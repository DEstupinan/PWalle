using System.Collections.Generic;
public class And : ExpresionBinaria
{
    public override ExpressionType Type { get; set; }
    public override object Value { get; set; }

    public And(CodeLocation location) : base(location) { }
    public override void Calculate()
    {
        Right.Calculate();
        Left.Calculate();
        if ((bool)Left.Value && (bool)Right.Value) Value = (bool)true;
        else Value = (bool)false;
    }

    public override bool Check(List<Error> err)
    {
        bool right = Right.Check(err);
        bool left = Left.Check(err);
        if (Right.Type != ExpressionType.Logic|| Left.Type != ExpressionType.Logic)
        {
            err.Add(new Error(Location, ErrorType.Invalid, "The operands must be bool expresions"));
            Type = ExpressionType.Error;
            return false;
        }
        Type = ExpressionType.Logic;
        return right && left;
    }
}