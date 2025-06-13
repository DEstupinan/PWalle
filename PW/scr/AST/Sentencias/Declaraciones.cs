public class Declaration : Statement
{

    public string Name;
    public Expresion Value;
    public Enviroment Env;

    public Declaration(CodeLocation location , string name, Expresion value,Enviroment env) : base(location)
    {

        Name = name;
        Value = value;
        Env=env;
    }
    public override void Execute()
    {   
        Value.Calculate();
        Env.variables[Name]=Value.Value;
    }
    public override bool Check(List<Error> err)
    {
        return true;
    }
}