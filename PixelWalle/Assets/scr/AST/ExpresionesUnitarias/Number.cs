using System.Collections.Generic;
public class Number : ExpresionUnitaria
{
     public override ExpressionType Type
    {
        get
        {
            return ExpressionType.Number;
        }
        set { }
    }
    public override object Value{ get; set; }
    public Number(double value, CodeLocation location) : base(location)
    {
        Value = value;
    }
    public override bool Check(List<Error> err)
    {
        return true;
    }
    public override void Calculate() { }

}