using System.Collections.Generic;
public class Label : Statement
{

    public string Name;
    public WProgram Program;

    public Label(CodeLocation location , string name ,WProgram program) : base(location)
    {
        Name = name;
        Program = program;
    }
    public override void Execute(List<Error> err)
    {
      
        Program.Labels[Name]=Program.index;
      
        
    }
    public override bool Check(List<Error> err)
    {   
        if( Program.Labels.ContainsKey(Name))
        {
            err.Add(new Error(Location, ErrorType.Invalid, "Label alredy declarated"));
            return false;
        }
       
        return true;
    }
}