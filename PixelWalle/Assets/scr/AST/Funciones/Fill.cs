using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
public  class Fill : Function
{
   
 private Pintar pintar;
    public Fill(Pintar pintar)
    {
        this.pintar = pintar;
    }
    public override void Call(List<Expresion> arguments)
    {
        pintar.Fill();
    }
    public override bool Check(CodeLocation location, List<Error> err,List<Expresion> arguments)
    {   
         if (arguments.Count() != 0)
        {
            err.Add(new Error(location, ErrorType.Invalid, "Unexpected arguments"));
            return false;
        }
        return true;
    }
    
}