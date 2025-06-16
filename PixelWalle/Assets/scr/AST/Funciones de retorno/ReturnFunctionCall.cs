using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
public class ReturnFunctionCall : Expresion
{
    public List<Expresion> Arguments;
    public string Name;
    public override ExpressionType Type
    {
        get
        {
           return Env.returnfunctions[Name].Type;
        }
        set { }
    }
    public override object Value { get; set; }
    public Enviroment Env;
    public ReturnFunctionCall(CodeLocation location, List<Expresion> arguments, string name, Enviroment env) : base(location)
    {
        Name = name;
        Arguments = arguments;
        Env = env;
    }
    public override void Calculate()
    {
        
            Value = Env.returnfunctions[Name].Call(Arguments);
        
    }

    public override bool Check(List<Error> err)
    {
        return Env.returnfunctions[Name].Check(Location, err, Arguments);
    }
}