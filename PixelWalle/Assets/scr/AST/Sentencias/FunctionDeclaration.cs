using System.Collections.Generic;
public class FunctionDeclaration : Statement
{   
    public List<Expresion> Arguments;
    public string Name;
    public Enviroment Env;
    public FunctionDeclaration(CodeLocation location,List<Expresion> arguments,string name,Enviroment env) : base(location) 
    {
        Name=name;
        Arguments = arguments;
        Env=env;
     }
    public override void Execute(List<Error> err)
    {
        
            Env.functions[Name].Call(Arguments);
        
    }
   
    public override bool Check(List<Error> err)
    {
        return Env.functions[Name].Check(Location,err,Arguments);
    }
}