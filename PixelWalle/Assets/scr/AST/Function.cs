using System.Collections.Generic;
public abstract class Function 
{
    public abstract void Call(List<Expresion> arguments);
    public abstract bool Check(CodeLocation location, List<Error> err,List<Expresion> arguments);

}
