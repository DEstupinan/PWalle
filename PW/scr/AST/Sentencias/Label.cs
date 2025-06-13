public class Label : Statement
{

    public string Name;
    public WProgram Program;

    public Label(CodeLocation location , string name ,WProgram program) : base(location)
    {
        Name = name;
        Program = program;
    }
    public override void Execute()
    {
       
        
    }
    public override bool Check(List<Error> err)
    {
        return true;
    }
}