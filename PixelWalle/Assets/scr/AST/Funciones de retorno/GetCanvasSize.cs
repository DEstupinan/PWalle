using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
public class GetCanvasSize : ReturnFunction
{
     private Pintar pintar;
    public GetCanvasSize(Pintar pintar)
    {
        this.pintar = pintar;
    }
    public override object Call(List<Expresion> arguments)
    {
        return pintar.GetCanvasSize();
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
         if (arguments.Count() != 0)
        {
            err.Add(new Error(location, ErrorType.Invalid, "Unexpected arguments"));
            return false;
        }
        return true;
    }
    
}