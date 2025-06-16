using System.Collections.Generic;
public class LogicNegative : ExpresionUnary
{
    public override ExpressionType Type { get; set; }
    public override object Value { get; set; }

    public LogicNegative(CodeLocation location) : base(location)
    {
       
    }
    public override void Calculate()
    {
        Right.Calculate();

        Value = !(bool)Right.Value;
    }

    public override bool Check(List<Error> err)
    {
        bool right = Right.Check(err);

        if (Right.Type != ExpressionType.Logic)
        {
            err.Add(new Error(Location, ErrorType.Invalid, "The operands must be bool expresions"));
            Type = ExpressionType.Error;
            return false;
        }
        Type = ExpressionType.Logic;
        return right;
    }
}