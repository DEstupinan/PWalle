using System.Collections.Generic;
public class Literal : ExpresionUnitaria
{
    public Enviroment Env;
    public string Name;
    public override ExpressionType Type
    {
        get
        {
            if (Env.Get(Name) is bool) 
            return ExpressionType.Logic;
            else if(Env.Get(Name) is string || Env.Get(Name) is char)return ExpressionType.Text;
            else return ExpressionType.Number;
        }
        set { }
    }

    public override object Value { get; set; }

    public Literal(string name, Enviroment env, CodeLocation location) : base(location)
    {
        Name = name;
        Env = env;

    }

    public override bool Check(List<Error> err)
    {
        if (!Env.variables.ContainsKey(Name))
        {
            err.Add(new Error(Location, ErrorType.Invalid, "Undeclared variable"));
            Type = ExpressionType.Error;
            return false;
        }
        return true;
    }
    public override void Calculate()
    {
        Value = Env.Get(Name);
    }
}