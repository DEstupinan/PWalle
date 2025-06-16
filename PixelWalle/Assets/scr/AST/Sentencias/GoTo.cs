using System.Collections.Generic;
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
    public override void Execute(List<Error> err)
    {
        


            Program.index = Program.Labels[Label];


        

    }
    public override bool Check(List<Error> err)
    {
        bool ret = true;

        if (!Program.Labels.ContainsKey(Label))
        {
            err.Add(new Error(Location, ErrorType.Invalid, "Label not declarated"));

            ret = false;
        }
        if (Condition.Check(err))
        {
            Condition.Calculate();
        }

        if (Condition.Type != ExpressionType.Logic)
        {
            err.Add(new Error(Condition.Location, ErrorType.Invalid, "Expected bool expresion"));

            ret = false;
        }


        return ret;
    }
}