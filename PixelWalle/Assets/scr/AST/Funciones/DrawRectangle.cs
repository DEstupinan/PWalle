using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
public class DrawRectangle : Function
{
    private Pintar pintar;
    public DrawRectangle(Pintar pintar)
    {
        this.pintar = pintar;
    }

    public override void Call(List<Expresion> arguments)
    {
        pintar.DrawRectangle((int)arguments[0].Value,
         (int)arguments[1].Value, (int)arguments[2].Value,(int)arguments[3].Value,
          (int)arguments[4].Value);
    }
    public override bool Check(CodeLocation location, List<Error> err, List<Expresion> arguments)
    {
        bool ret = true;
        if (arguments.Count() != 5)
        {
            err.Add(new Error(location, ErrorType.Invalid, "Expected 5 arguments"));
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
        for (int i = 0; i < 2; i++)
        {
            if (arguments[i].Type != ExpressionType.Number || ((int)arguments[i].Value != 0 && (int)arguments[i].Value != 1 && (int)arguments[i].Value != -1))
            {
                err.Add(new Error(arguments[i].Location, ErrorType.Invalid, $"Argument {i + 1} must be '-1', '0' or '1'"));
                ret = false;
            }

        }
         for (int i = 2; i < 5; i++)
        {
            if (arguments[i].Type != ExpressionType.Number ||  (int)arguments[i].Value<0)
            {
                err.Add(new Error(arguments[i].Location, ErrorType.Invalid, $"Argument {i + 1} must be positive expresion"));
                ret = false;
            }

        }
        
        return ret;
    }

}