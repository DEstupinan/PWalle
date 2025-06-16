using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
public abstract class ReturnFunction 
{
    public abstract ExpressionType Type{ get; set; }
    public abstract object Call(List<Expresion> arguments);
    public abstract  bool Check(CodeLocation location, List<Error> err, List<Expresion> arguments);
}