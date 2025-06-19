using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
public class IsBrushSize : ReturnFunction
{
    private Pintar pintar;
    public IsBrushSize(Pintar pintar)
    {
        this.pintar = pintar;
    }
    public override object Call(List<Expresion> arguments)
    {
        return pintar.IsBrushSize((int)arguments[0].Value);
    }

    public override ExpressionType Type
    {
        get
        {
            return ExpressionType.Number;
        }
        set { }
    }
    public override bool Check(CodeLocation location, List<Error> err, List<Expresion> arguments)
    {
        bool ret = true;
        if (arguments.Count() != 1)
        {
            err.Add(new Error(location, ErrorType.Invalid, "Expected 1 arguments"));
            return false;
        }
        foreach (Expresion e in arguments)
        {
            if (e.Check(err))
            {
                e.Calculate();
            }
            else return false;
        }
         if (arguments[0].Type != ExpressionType.Number ||  (int)arguments[0].Value<0)
            {
                err.Add(new Error(arguments[0].Location, ErrorType.Invalid, $"Argument 1 must be positive expresion"));
                ret = false;
            }
        return ret;
    }


}