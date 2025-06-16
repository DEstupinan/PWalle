using System.Collections.Generic;
public class Negative : ExpresionUnary
{
    public override ExpressionType Type { get; set; }
    public override object Value { get; set; }

    public Negative(CodeLocation location) : base(location) 
    {
        
     }
    public override void Calculate()
    {
        Right.Calculate();

        Value = (int)Right.Value * (-1);
    }

    public override bool Check(List<Error> err)
    {
        bool right = Right.Check(err);

        if (Right.Type != ExpressionType.Number)
        {
            err.Add(new Error(Location, ErrorType.Invalid, "The operands must be numbers"));
            Type = ExpressionType.Error;
            return false;
        }
        Type = ExpressionType.Number;
        return right;
    }
}