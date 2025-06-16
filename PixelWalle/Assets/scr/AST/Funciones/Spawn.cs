using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
public class Spawn : Function
{
    bool used = false;
    private Pintar pintar;
    public Spawn(Pintar pintar)
    {
        this.pintar = pintar;
    }
    public override void Call(List<Expresion> arguments)
    {
        used = true;
        pintar.Spawn(Convert.ToInt32(arguments[0].Value), Convert.ToInt32(arguments[1].Value));
    }
    public override bool Check(CodeLocation location, List<Error> err, List<Expresion> arguments)
    {
        if (used)
        {
            err.Add(new Error(location, ErrorType.Invalid, "Spawn alredy declarated"));
            return false;
        }
        bool ret = true;
        if (arguments.Count() != 2)
        {
            err.Add(new Error(location, ErrorType.Invalid, "Expected 2 arguments"));
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
        for (int i = 0; i < arguments.Count(); i++)
        {
            if (arguments[i].Type != ExpressionType.Number)
            {
                err.Add(new Error(arguments[i].Location, ErrorType.Invalid, $"Argument {i + 1} must be numerical expression"));
                ret = false;
            }
            if (!ret) return false;
        }
        if (Convert.ToInt32(arguments[0].Value) < 0
            || Convert.ToInt32(arguments[0].Value) >= pintar.board.boardSize)
        {
            err.Add(new Error(arguments[0].Location, ErrorType.Invalid, $"Invalid position"));
            ret = false;
        }
        if (Convert.ToInt32(arguments[1].Value) < 0
           || Convert.ToInt32(arguments[1].Value) >= pintar.board.boardSize)
        {
            err.Add(new Error(arguments[1].Location, ErrorType.Invalid, $"Invalid position"));
            ret = false;



        }
        return ret;
    }
}