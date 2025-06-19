using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
using UnityEngine;
public class DrawLine : Function
{
    private Pintar pintar;
    public DrawLine(Pintar pintar)
    {
        this.pintar = pintar;
    }

    public override void Call(List<Expresion> arguments)
    {

        pintar.DrawLine(Convert.ToInt32(arguments[0].Value), Convert.ToInt32(arguments[1].Value), Convert.ToInt32(arguments[2].Value));
    }
    public override bool Check(CodeLocation location, List<Error> err, List<Expresion> arguments)
    {
        bool ret = true;
        if (arguments.Count() != 3)
        {
            err.Add(new Error(location, ErrorType.Invalid, "Expected 3 arguments"));
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

        for (int i = 0; i < arguments.Count() - 1; i++)
        {
            if (arguments[i].Type != ExpressionType.Number || ((int)arguments[i].Value != 0 && (int)arguments[i].Value != 1 && (int)arguments[i].Value != -1))

            {
                err.Add(new Error(arguments[i].Location, ErrorType.Invalid, $"Argument {i + 1} must be '-1', '0' or '1'"));
                ret = false;
            }

        }
        if (arguments[2].Type != ExpressionType.Number ||  (int)arguments[2].Value<0)
            {
                err.Add(new Error(arguments[2].Location, ErrorType.Invalid, $"Argument 3 must be positive expresion"));
                ret = false;
            }
        return ret;
    }

}