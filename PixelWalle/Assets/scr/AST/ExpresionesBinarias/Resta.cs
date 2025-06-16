using System.Collections.Generic;
public class Resta : ExpresionBinaria
{
    public override ExpressionType Type { get; set; }
    public override object Value { get; set; }

    public Resta(CodeLocation location) : base(location) { }
    public override void Calculate()
    {
        Right.Calculate();
        Left.Calculate();
        Value = (double)Left.Value - (double)Right.Value;
    }
    public override bool Check(List<Error> err)
    {
        bool right = Right.Check(err);
        bool left = Left.Check(err);
        if (Right.Type != ExpressionType.Number || Left.Type != ExpressionType.Number)
        {
            err.Add(new Error(Location, ErrorType.Invalid, "The operands must be numbers"));
            Type = ExpressionType.Error;
            return false;
        }
        Type = ExpressionType.Number;
        return right && left;
    }

}