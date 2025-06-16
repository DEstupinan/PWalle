using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
public  class Size : Function
{
   
 private Pintar pintar;
    public Size(Pintar pintar)
    {
        this.pintar = pintar;
    }
    public override void Call(List<Expresion> arguments)
    {
        pintar.Size((int)arguments[0].Value);
    }
    public override bool Check(CodeLocation location, List<Error> err,List<Expresion> arguments)
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
        if (arguments[0].Type != ExpressionType.Number)
        {
            err.Add(new Error(arguments[0].Location, ErrorType.Invalid, "Argument 1 must be numerical expresion"));
            return  false;
        }
        if ((int)arguments[0].Value <1 )
        {
            err.Add(new Error(arguments[0].Location, ErrorType.Invalid, "Argument 1  must be greater than 1"));
            return  false;
        }
        return ret;
    }
    
}