using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class ChangeColor : Function
{
    private Pintar pintar;
    public ChangeColor(Pintar pintar)
    {
        this.pintar = pintar;
    }
    List<string> colors = new List<string>{
        "Red",
        "Blue",
        "Green",
        "Yellow",
        "Orange",
        "Purple",
        "Black",
        "White",
        "Transparent",

    };

    public override void Call(List<Expresion> arguments)
    {
        pintar.ChangeColor((string)arguments[0].Value);
    }
    public override bool Check(CodeLocation location, List<Error> err,List<Expresion> arguments)
    {
        
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
        if (arguments[0].Type != ExpressionType.Text)
        {
            err.Add(new Error(arguments[0].Location, ErrorType.Invalid, "Argument 1 must be string expresion"));
             return false;
        }
        if(!colors.Contains(arguments[0].Value))
        {
            err.Add(new Error(arguments[0].Location, ErrorType.Invalid, "Uknow color"));
           return false;
        }

        return true;
    }

}