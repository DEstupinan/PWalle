public class GoTo : Statement
{

    public Expresion Condition;
    public string Label;
    public WProgram Program;

    public GoTo(CodeLocation location, Expresion condition, string label, WProgram program) : base(location)
    {
        Condition = condition;
        Label = label;
        Program = program;
    }
    public override void Execute()
    {   
        Condition.Calculate();
        if((bool)Condition.Value)
        {
            Program.index=Program.Labels[Label];
        }
    }
    public override bool Check(List<Error> err)
    {
        return true;
    }
}