using System.Collections.Generic;
public class Text : ExpresionUnitaria
{

    public override ExpressionType Type
    {
        get
        {
            return ExpressionType.Text;
        }
        set { }
    }

    public override object Value { get; set; }
    
    public Text(string value, CodeLocation location) : base(location)
    {
        Value = value;
    }
    
    public override bool Check( List<Error> errors)
    {
        return true;
    }

    public override void Calculate() { }

    
}